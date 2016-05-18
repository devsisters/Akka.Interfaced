﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence;

namespace Akka.Interfaced.Persistence
{
    public abstract class InterfacedPersistentActor<T> : UntypedPersistentActor, IRequestWaiter, IFilterPerInstanceProvider
        where T : InterfacedPersistentActor<T>
    {
        #region Static Variables

        private readonly static RequestDispatcher<T> RequestDispatcher;
        private readonly static NotificationDispatcher<T> NotificationDispatcher;
        private readonly static MessageDispatcher<T> MessageDispatcher;
        private readonly static List<Func<object, IFilter>> PerInstanceFilterCreators;

        static InterfacedPersistentActor()
        {
            var filterHandlerBuilder = new FilterHandlerBuilder(typeof(T));

            var requestHandlerBuilder = new RequestHandlerBuilder<T>();
            RequestDispatcher = new RequestDispatcher<T>(requestHandlerBuilder.Build(filterHandlerBuilder));

            var notificationHandlerBuilder = new NotificationHandlerBuilder<T>();
            NotificationDispatcher = new NotificationDispatcher<T>(notificationHandlerBuilder.Build(filterHandlerBuilder));

            MessageDispatcher = new MessageDispatcher<T>();

            PerInstanceFilterCreators = filterHandlerBuilder.PerInstanceFilterCreators;
        }

        #endregion

        #region Member Variables

        private int _activeReentrantCount;
        private MessageHandleContext _currentAtomicContext;
        private InterfacedActorRequestWaiter _requestWaiter;
        private InterfacedActorObserverMap _observerMap;
        private InterfacedActorPerInstanceFilterList _perInstanceFilterList;
        private Dictionary<long, TaskCompletionSource<SnapshotMetadata>> _saveSnapshotTcsMap;

        #endregion

        protected new IActorRef Sender
        {
            get
            {
                var context = ActorSynchronizationContext.GetCurrentContext();
                return context != null ? context.Sender : base.Sender;
            }
        }

        // Atomic async OnPreStart event (it will be called after PreStart)
        protected virtual Task OnPreStart()
        {
            return Task.FromResult(true);
        }

        // Atomic async OnPreStop event (it will be called when receives StopMessage)
        // After finishing this call, actor will be stopped.
        protected virtual Task OnPreStop()
        {
            return Task.FromResult(true);
        }

        protected override void PreStart()
        {
            if (PerInstanceFilterCreators.Count > 0)
                _perInstanceFilterList = new InterfacedActorPerInstanceFilterList(this, PerInstanceFilterCreators);

            InvokeOnPreStart();
            base.PreStart();
        }

        private void InvokeOnPreStart()
        {
            var context = new MessageHandleContext { Self = Self, Sender = base.Sender };
            BecomeStacked(OnReceiveInAtomicTask);
            _currentAtomicContext = context;

            using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(context)))
            {
                OnPreStart().ContinueWith(t => OnTaskCompleted(false),
                                          TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        private void InvokeOnPreStop()
        {
            var context = new MessageHandleContext { Self = Self };
            BecomeStacked(OnReceiveInAtomicTask);
            _currentAtomicContext = context;

            using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(context)))
            {
                OnPreStop().ContinueWith(t => OnTaskCompleted(false, true),
                                         TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        protected override void OnRecover(object message)
        {
            var messageHandler = MessageDispatcher.GetHandler(message.GetType());
            if (messageHandler != null)
            {
                HandleMessageByHandler(message, messageHandler);
                return;
            }

            OnReceiveUnhandled(message);
        }

        protected override void OnCommand(object message)
        {
            var requestMessage = message as RequestMessage;
            if (requestMessage != null)
            {
                OnRequestMessage(requestMessage);
                return;
            }

            var continuationMessage = message as TaskContinuationMessage;
            if (continuationMessage != null)
            {
                OnTaskContinuationMessage(continuationMessage);
                return;
            }

            var responseMessage = message as ResponseMessage;
            if (responseMessage != null)
            {
                OnResponseMessage(responseMessage);
                return;
            }

            var notificationMessage = message as NotificationMessage;
            if (notificationMessage != null)
            {
                OnNotificationMessage(notificationMessage);
                return;
            }

            var taskRunMessage = message as TaskRunMessage;
            if (taskRunMessage != null)
            {
                OnTaskRunMessage(taskRunMessage);
                return;
            }

            var poisonPill = message as InterfacedPoisonPill;
            if (poisonPill != null)
            {
                OnInterfacedPoisonPill();
                return;
            }

            var messageHandler = MessageDispatcher.GetHandler(message.GetType());
            if (messageHandler != null)
            {
                HandleMessageByHandler(message, messageHandler);
                return;
            }

            if (HandleSnapshotResultMessages(message))
                return;

            OnReceiveUnhandled(message);
        }

        private void OnRequestMessage(RequestMessage request)
        {
            var sender = base.Sender;

            var handlerItem = RequestDispatcher.GetHandler(request.InvokePayload.GetType());
            if (handlerItem == null)
            {
                sender.Tell(new ResponseMessage
                {
                    RequestId = request.RequestId,
                    Exception = new InvalidMessageException("Cannot find handler")
                });
                return;
            }

            if (handlerItem.Handler != null)
            {
                // sync handle

                var response = handlerItem.Handler((T)this, request, null);
                if (request.RequestId != 0)
                    sender.Tell(response);
            }
            else
            {
                // async handle

                var context = new MessageHandleContext { Self = Self, Sender = base.Sender };
                if (handlerItem.IsReentrant)
                {
                    _activeReentrantCount += 1;
                }
                else
                {
                    BecomeStacked(OnReceiveInAtomicTask);
                    _currentAtomicContext = context;
                }

                using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(context)))
                {
                    var requestId = request.RequestId;
                    var isReentrant = handlerItem.IsReentrant;
                    handlerItem.AsyncHandler((T)this, request, response =>
                    {
                        if (requestId != 0)
                            sender.Tell(response);
                        OnTaskCompleted(isReentrant);
                    });
                }
            }
        }

        private static void OnTaskContinuationMessage(TaskContinuationMessage continuation)
        {
            using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(continuation.Context)))
            {
                continuation.CallbackAction(continuation.CallbackState);
            }
        }

        private void OnResponseMessage(ResponseMessage response)
        {
            _requestWaiter?.OnResponseMessage(response, _currentAtomicContext);
        }

        private void OnNotificationMessage(NotificationMessage notification)
        {
            _observerMap?.Notify(notification);
        }

        private void OnTaskRunMessage(TaskRunMessage taskRunMessage)
        {
            var context = new MessageHandleContext { Self = Self, Sender = base.Sender };
            if (taskRunMessage.IsReentrant)
            {
                _activeReentrantCount += 1;
            }
            else
            {
                BecomeStacked(OnReceiveInAtomicTask);
                _currentAtomicContext = context;
            }

            using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(context)))
            {
                taskRunMessage.Function()
                              .ContinueWith(t => OnTaskCompleted(taskRunMessage.IsReentrant),
                                            TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        private void OnInterfacedPoisonPill()
        {
            if (_activeReentrantCount > 0)
            {
                BecomeStacked(OnReceiveInWaitingForReentrantsFinished);
            }
            else
            {
                InvokeOnPreStop();
            }
        }

        private void HandleMessageByHandler(object message, MessageHandlerItem<T> handlerItem)
        {
            if (handlerItem.AsyncHandler != null)
            {
                var context = new MessageHandleContext { Self = Self, Sender = base.Sender };
                if (handlerItem.IsReentrant)
                {
                    _activeReentrantCount += 1;
                }
                else
                {
                    BecomeStacked(OnReceiveInAtomicTask);
                    _currentAtomicContext = context;
                }

                using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(context)))
                {
                    handlerItem.AsyncHandler((T)this, message)
                               .ContinueWith(t => OnTaskCompleted(handlerItem.IsReentrant),
                                             TaskContinuationOptions.ExecuteSynchronously);
                }
            }
            else
            {
                handlerItem.Handler((T)this, message);
            }
        }

        private void OnReceiveInAtomicTask(object message)
        {
            var continuationMessage = message as TaskContinuationMessage;
            if (continuationMessage != null && continuationMessage.Context == _currentAtomicContext)
            {
                using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(continuationMessage.Context)))
                {
                    continuationMessage.CallbackAction(continuationMessage.CallbackState);
                }
                return;
            }

            var response = message as ResponseMessage;
            if (response != null)
            {
                OnResponseMessage(response);
                return;
            }

            if (HandleSnapshotResultMessages(message))
                return;

            Stash.Stash();
        }

        private void OnReceiveInWaitingForReentrantsFinished(object message)
        {
            var continuationMessage = message as TaskContinuationMessage;
            if (continuationMessage != null)
            {
                using (new SynchronizationContextSwitcher(new ActorSynchronizationContext(continuationMessage.Context)))
                {
                    continuationMessage.CallbackAction(continuationMessage.CallbackState);
                }

                if (_activeReentrantCount == 0)
                {
                    UnbecomeStacked();
                    InvokeOnPreStop();
                }
                return;
            }

            var response = message as ResponseMessage;
            if (response != null)
            {
                OnResponseMessage(response);
                return;
            }

            Stash.Stash();
        }

        private void OnTaskCompleted(bool isReentrant, bool stopOnCompleted = false)
        {
            if (isReentrant)
            {
                _activeReentrantCount -= 1;
            }
            else
            {
                _currentAtomicContext = null;
                UnbecomeStacked();
                Stash.UnstashAll();
            }

            if (stopOnCompleted)
            {
                Context.Stop(Self);
            }
        }

        // from IRequestWaiter

        void IRequestWaiter.SendRequest(IActorRef target, RequestMessage requestMessage)
        {
            target.Tell(requestMessage);
        }

        Task IRequestWaiter.SendRequestAndWait(
            IActorRef target, RequestMessage request, TimeSpan? timeout)
        {
            if (_requestWaiter == null)
                _requestWaiter = new InterfacedActorRequestWaiter();

            return _requestWaiter.SendRequestAndWait(target, request, Self, timeout);
        }

        Task<TReturn> IRequestWaiter.SendRequestAndReceive<TReturn>(
            IActorRef target, RequestMessage request, TimeSpan? timeout)
        {
            if (_requestWaiter == null)
                _requestWaiter = new InterfacedActorRequestWaiter();

            return _requestWaiter.SendRequestAndReceive<TReturn>(target, request, Self, timeout);
        }

        // async support

        protected void RunTask(Action action)
        {
            RunTask(() =>
            {
                action();
                return Task.FromResult(0);
            });
        }

        protected void RunTask(Func<Task> function)
        {
            RunTask(function, false);
        }

        protected void RunTask(Func<Task> function, bool isReentrant)
        {
            Self.Tell(new TaskRunMessage { Function = function, IsReentrant = isReentrant });
        }

        // other messages

        protected virtual void OnReceiveUnhandled(object message)
        {
            Unhandled(message);
        }

        // observer support

        private InterfacedActorObserverMap EnsureObserverMap()
        {
            return _observerMap ?? (_observerMap = new InterfacedActorObserverMap());
        }

        protected int IssueObserverId()
        {
            return EnsureObserverMap().IssueId();
        }

        protected void AddObserver(int observerId, IInterfacedObserver observer)
        {
            EnsureObserverMap().Add(observerId, observer);
        }

        protected IInterfacedObserver GetObserver(int observerId)
        {
            return _observerMap?.Get(observerId);
        }

        protected bool RemoveObserver(int observerId)
        {
            return _observerMap?.Remove(observerId) ?? false;
        }

        // PerInstance Filter related

        IFilter IFilterPerInstanceProvider.GetFilter(int index)
        {
            return _perInstanceFilterList.Get(index);
        }

        // Additional persistent features

        protected Task PersistTaskAsync<TEvent>(TEvent @event)
        {
            var tcs = new TaskCompletionSource<bool>();
            Persist(@event, _ => tcs.SetResult(true));
            return tcs.Task;
        }

        protected Task PersistTaskAsync<TEvent>(IEnumerable<TEvent> events, Action<TEvent> handler)
        {
            var tcs = new TaskCompletionSource<bool>();
            PersistAll(events, _ => tcs.SetResult(true));
            return tcs.Task;
        }

        protected Task<SnapshotMetadata> SaveSnapshotTaskAsync(object snapshot)
        {
            if (_saveSnapshotTcsMap == null)
                _saveSnapshotTcsMap = new Dictionary<long, TaskCompletionSource<SnapshotMetadata>>();

            var metadata = new SnapshotMetadata(SnapshotterId, SnapshotSequenceNr);
            if (_saveSnapshotTcsMap.ContainsKey(SnapshotSequenceNr))
                return Task.FromResult(metadata);

            var tcs = new TaskCompletionSource<SnapshotMetadata>();
            _saveSnapshotTcsMap.Add(SnapshotSequenceNr, tcs);

            SnapshotStore.Tell(new SaveSnapshot(metadata, snapshot), Self);
            return tcs.Task;
        }

        protected bool HandleSnapshotResultMessages(object message)
        {
            var success = message as SaveSnapshotSuccess;
            if (success != null)
            {
                var seq = success.Metadata.SequenceNr;
                TaskCompletionSource<SnapshotMetadata> tcs;
                if (_saveSnapshotTcsMap.TryGetValue(seq, out tcs))
                {
                    _saveSnapshotTcsMap.Remove(seq);
                    tcs.SetResult(success.Metadata);
                }
                return true;
            }

            var failure = message as SaveSnapshotFailure;
            if (failure != null)
            {
                var seq = failure.Metadata.SequenceNr;
                TaskCompletionSource<SnapshotMetadata> tcs;
                if (_saveSnapshotTcsMap.TryGetValue(seq, out tcs))
                {
                    _saveSnapshotTcsMap.Remove(seq);
                    tcs.SetException(failure.Cause ?? new Exception("No Exception"));
                }
                return true;
            }

            return false;
        }
    }
}
