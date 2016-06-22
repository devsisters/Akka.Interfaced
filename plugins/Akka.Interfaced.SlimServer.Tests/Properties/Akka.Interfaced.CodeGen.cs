﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Akka.Interfaced CodeGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Akka.Interfaced;
using Akka.Actor;

#region Akka.Interfaced.SlimServer.IDummy

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(IDummy), PayloadTableKind.Request)]
    public static class IDummy_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(Call_Invoke), typeof(Call_Return) },
            };
        }

        public class Call_Invoke
            : IInterfacedPayload, IAsyncInvokable
        {
            public System.Object param;

            public Type GetInterfaceType()
            {
                return typeof(IDummy);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                var __v = await ((IDummy)__target).Call(param);
                return (IValueGetable)(new Call_Return { v = __v });
            }
        }

        public class Call_Return
            : IInterfacedPayload, IValueGetable
        {
            public System.Object v;

            public Type GetInterfaceType()
            {
                return typeof(IDummy);
            }

            public object Value
            {
                get { return v; }
            }
        }
    }

    public interface IDummy_NoReply
    {
        void Call(System.Object param);
    }

    public class DummyRef : InterfacedActorRef, IDummy, IDummy_NoReply
    {
        public DummyRef() : base(null)
        {
        }

        public DummyRef(IRequestTarget target) : base(target)
        {
        }

        public DummyRef(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public DummyRef(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public DummyRef(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public IDummy_NoReply WithNoReply()
        {
            return this;
        }

        public DummyRef WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new DummyRef(Target, requestWaiter, Timeout);
        }

        public DummyRef WithTimeout(TimeSpan? timeout)
        {
            return new DummyRef(Target, RequestWaiter, timeout);
        }

        public Task<System.Object> Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        void IDummy_NoReply.Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(IDummy))]
    public interface IDummySync : IInterfacedActorSync
    {
        System.Object Call(System.Object param);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.IDummyEx

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(IDummyEx), PayloadTableKind.Request)]
    public static class IDummyEx_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(CallEx_Invoke), typeof(CallEx_Return) },
            };
        }

        public class CallEx_Invoke
            : IInterfacedPayload, IAsyncInvokable
        {
            public System.Object param;

            public Type GetInterfaceType()
            {
                return typeof(IDummyEx);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                var __v = await ((IDummyEx)__target).CallEx(param);
                return (IValueGetable)(new CallEx_Return { v = __v });
            }
        }

        public class CallEx_Return
            : IInterfacedPayload, IValueGetable
        {
            public System.Object v;

            public Type GetInterfaceType()
            {
                return typeof(IDummyEx);
            }

            public object Value
            {
                get { return v; }
            }
        }
    }

    public interface IDummyEx_NoReply : IDummy_NoReply
    {
        void CallEx(System.Object param);
    }

    public class DummyExRef : InterfacedActorRef, IDummyEx, IDummyEx_NoReply
    {
        public DummyExRef() : base(null)
        {
        }

        public DummyExRef(IRequestTarget target) : base(target)
        {
        }

        public DummyExRef(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public DummyExRef(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public DummyExRef(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public IDummyEx_NoReply WithNoReply()
        {
            return this;
        }

        public DummyExRef WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new DummyExRef(Target, requestWaiter, Timeout);
        }

        public DummyExRef WithTimeout(TimeSpan? timeout)
        {
            return new DummyExRef(Target, RequestWaiter, timeout);
        }

        public Task<System.Object> CallEx(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx_PayloadTable.CallEx_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        public Task<System.Object> Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        void IDummyEx_NoReply.CallEx(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx_PayloadTable.CallEx_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IDummy_NoReply.Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(IDummyEx))]
    public interface IDummyExSync : IDummySync
    {
        System.Object CallEx(System.Object param);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.IDummyEx2

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(IDummyEx2), PayloadTableKind.Request)]
    public static class IDummyEx2_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(CallEx2_Invoke), typeof(CallEx2_Return) },
            };
        }

        public class CallEx2_Invoke
            : IInterfacedPayload, IAsyncInvokable
        {
            public System.Object param;

            public Type GetInterfaceType()
            {
                return typeof(IDummyEx2);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                var __v = await ((IDummyEx2)__target).CallEx2(param);
                return (IValueGetable)(new CallEx2_Return { v = __v });
            }
        }

        public class CallEx2_Return
            : IInterfacedPayload, IValueGetable
        {
            public System.Object v;

            public Type GetInterfaceType()
            {
                return typeof(IDummyEx2);
            }

            public object Value
            {
                get { return v; }
            }
        }
    }

    public interface IDummyEx2_NoReply : IDummy_NoReply
    {
        void CallEx2(System.Object param);
    }

    public class DummyEx2Ref : InterfacedActorRef, IDummyEx2, IDummyEx2_NoReply
    {
        public DummyEx2Ref() : base(null)
        {
        }

        public DummyEx2Ref(IRequestTarget target) : base(target)
        {
        }

        public DummyEx2Ref(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public DummyEx2Ref(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public DummyEx2Ref(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public IDummyEx2_NoReply WithNoReply()
        {
            return this;
        }

        public DummyEx2Ref WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new DummyEx2Ref(Target, requestWaiter, Timeout);
        }

        public DummyEx2Ref WithTimeout(TimeSpan? timeout)
        {
            return new DummyEx2Ref(Target, RequestWaiter, timeout);
        }

        public Task<System.Object> CallEx2(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx2_PayloadTable.CallEx2_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        public Task<System.Object> Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        void IDummyEx2_NoReply.CallEx2(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx2_PayloadTable.CallEx2_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IDummy_NoReply.Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(IDummyEx2))]
    public interface IDummyEx2Sync : IDummySync
    {
        System.Object CallEx2(System.Object param);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.IDummyExFinal

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(IDummyExFinal), PayloadTableKind.Request)]
    public static class IDummyExFinal_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(CallExFinal_Invoke), typeof(CallExFinal_Return) },
            };
        }

        public class CallExFinal_Invoke
            : IInterfacedPayload, IAsyncInvokable
        {
            public System.Object param;

            public Type GetInterfaceType()
            {
                return typeof(IDummyExFinal);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                var __v = await ((IDummyExFinal)__target).CallExFinal(param);
                return (IValueGetable)(new CallExFinal_Return { v = __v });
            }
        }

        public class CallExFinal_Return
            : IInterfacedPayload, IValueGetable
        {
            public System.Object v;

            public Type GetInterfaceType()
            {
                return typeof(IDummyExFinal);
            }

            public object Value
            {
                get { return v; }
            }
        }
    }

    public interface IDummyExFinal_NoReply : IDummyEx_NoReply, IDummy_NoReply, IDummyEx2_NoReply
    {
        void CallExFinal(System.Object param);
    }

    public class DummyExFinalRef : InterfacedActorRef, IDummyExFinal, IDummyExFinal_NoReply
    {
        public DummyExFinalRef() : base(null)
        {
        }

        public DummyExFinalRef(IRequestTarget target) : base(target)
        {
        }

        public DummyExFinalRef(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public DummyExFinalRef(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public DummyExFinalRef(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public IDummyExFinal_NoReply WithNoReply()
        {
            return this;
        }

        public DummyExFinalRef WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new DummyExFinalRef(Target, requestWaiter, Timeout);
        }

        public DummyExFinalRef WithTimeout(TimeSpan? timeout)
        {
            return new DummyExFinalRef(Target, RequestWaiter, timeout);
        }

        public Task<System.Object> CallExFinal(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyExFinal_PayloadTable.CallExFinal_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        public Task<System.Object> CallEx(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx_PayloadTable.CallEx_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        public Task<System.Object> Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        public Task<System.Object> CallEx2(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx2_PayloadTable.CallEx2_Invoke { param = param }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        void IDummyExFinal_NoReply.CallExFinal(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyExFinal_PayloadTable.CallExFinal_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IDummyEx_NoReply.CallEx(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx_PayloadTable.CallEx_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IDummy_NoReply.Call(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummy_PayloadTable.Call_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IDummyEx2_NoReply.CallEx2(System.Object param)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyEx2_PayloadTable.CallEx2_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(IDummyExFinal))]
    public interface IDummyExFinalSync : IDummyExSync, IDummySync, IDummyEx2Sync
    {
        System.Object CallExFinal(System.Object param);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.IDummyWithTag

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(IDummyWithTag), PayloadTableKind.Request)]
    public static class IDummyWithTag_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(CallWithTag_Invoke), typeof(CallWithTag_Return) },
            };
        }

        public class CallWithTag_Invoke
            : IInterfacedPayload, IAsyncInvokable, IPayloadTagOverridable
        {
            public System.Object param;
            public System.String id;

            public Type GetInterfaceType()
            {
                return typeof(IDummyWithTag);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                var __v = await ((IDummyWithTag)__target).CallWithTag(param, id);
                return (IValueGetable)(new CallWithTag_Return { v = __v });
            }

            void IPayloadTagOverridable.SetTag(object value)
            {
                id = (System.String)value;
            }
        }

        public class CallWithTag_Return
            : IInterfacedPayload, IValueGetable
        {
            public System.Object v;

            public Type GetInterfaceType()
            {
                return typeof(IDummyWithTag);
            }

            public object Value
            {
                get { return v; }
            }
        }
    }

    public interface IDummyWithTag_NoReply
    {
        void CallWithTag(System.Object param, System.String id = null);
    }

    public class DummyWithTagRef : InterfacedActorRef, IDummyWithTag, IDummyWithTag_NoReply
    {
        public DummyWithTagRef() : base(null)
        {
        }

        public DummyWithTagRef(IRequestTarget target) : base(target)
        {
        }

        public DummyWithTagRef(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public DummyWithTagRef(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public DummyWithTagRef(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public IDummyWithTag_NoReply WithNoReply()
        {
            return this;
        }

        public DummyWithTagRef WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new DummyWithTagRef(Target, requestWaiter, Timeout);
        }

        public DummyWithTagRef WithTimeout(TimeSpan? timeout)
        {
            return new DummyWithTagRef(Target, RequestWaiter, timeout);
        }

        public Task<System.Object> CallWithTag(System.Object param, System.String id = null)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyWithTag_PayloadTable.CallWithTag_Invoke { param = param, id = id }
            };
            return SendRequestAndReceive<System.Object>(requestMessage);
        }

        void IDummyWithTag_NoReply.CallWithTag(System.Object param, System.String id)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new IDummyWithTag_PayloadTable.CallWithTag_Invoke { param = param, id = id }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(IDummyWithTag))]
    public interface IDummyWithTagSync : IInterfacedActorSync
    {
        System.Object CallWithTag(System.Object param, System.String id = null);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.ISubject

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(ISubject), PayloadTableKind.Request)]
    public static class ISubject_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,] {
                { typeof(MakeEvent_Invoke), null },
                { typeof(Subscribe_Invoke), null },
                { typeof(Unsubscribe_Invoke), null },
            };
        }

        public class MakeEvent_Invoke
            : IInterfacedPayload, IAsyncInvokable
        {
            public System.String eventName;

            public Type GetInterfaceType()
            {
                return typeof(ISubject);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                await ((ISubject)__target).MakeEvent(eventName);
                return null;
            }
        }

        public class Subscribe_Invoke
            : IInterfacedPayload, IAsyncInvokable, IPayloadObserverUpdatable
        {
            public Akka.Interfaced.SlimServer.ISubjectObserver observer;

            public Type GetInterfaceType()
            {
                return typeof(ISubject);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                await ((ISubject)__target).Subscribe(observer);
                return null;
            }

            void IPayloadObserverUpdatable.Update(Action<IInterfacedObserver> updater)
            {
                if (observer != null)
                {
                    updater(observer);
                }
            }
        }

        public class Unsubscribe_Invoke
            : IInterfacedPayload, IAsyncInvokable, IPayloadObserverUpdatable
        {
            public Akka.Interfaced.SlimServer.ISubjectObserver observer;

            public Type GetInterfaceType()
            {
                return typeof(ISubject);
            }

            public async Task<IValueGetable> InvokeAsync(object __target)
            {
                await ((ISubject)__target).Unsubscribe(observer);
                return null;
            }

            void IPayloadObserverUpdatable.Update(Action<IInterfacedObserver> updater)
            {
                if (observer != null)
                {
                    updater(observer);
                }
            }
        }
    }

    public interface ISubject_NoReply
    {
        void MakeEvent(System.String eventName);
        void Subscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer);
        void Unsubscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer);
    }

    public class SubjectRef : InterfacedActorRef, ISubject, ISubject_NoReply
    {
        public SubjectRef() : base(null)
        {
        }

        public SubjectRef(IRequestTarget target) : base(target)
        {
        }

        public SubjectRef(IRequestTarget target, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(target, requestWaiter, timeout)
        {
        }

        public SubjectRef(IActorRef actor) : base(new AkkaActorTarget(actor))
        {
        }

        public SubjectRef(IActorRef actor, IRequestWaiter requestWaiter, TimeSpan? timeout = null) : base(new AkkaActorTarget(actor), requestWaiter, timeout)
        {
        }

        public IActorRef Actor => ((AkkaActorTarget)Target)?.Actor;

        public ISubject_NoReply WithNoReply()
        {
            return this;
        }

        public SubjectRef WithRequestWaiter(IRequestWaiter requestWaiter)
        {
            return new SubjectRef(Target, requestWaiter, Timeout);
        }

        public SubjectRef WithTimeout(TimeSpan? timeout)
        {
            return new SubjectRef(Target, RequestWaiter, timeout);
        }

        public Task MakeEvent(System.String eventName)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.MakeEvent_Invoke { eventName = eventName }
            };
            return SendRequestAndWait(requestMessage);
        }

        public Task Subscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.Subscribe_Invoke { observer = (SubjectObserver)observer }
            };
            return SendRequestAndWait(requestMessage);
        }

        public Task Unsubscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.Unsubscribe_Invoke { observer = (SubjectObserver)observer }
            };
            return SendRequestAndWait(requestMessage);
        }

        void ISubject_NoReply.MakeEvent(System.String eventName)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.MakeEvent_Invoke { eventName = eventName }
            };
            SendRequest(requestMessage);
        }

        void ISubject_NoReply.Subscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.Subscribe_Invoke { observer = (SubjectObserver)observer }
            };
            SendRequest(requestMessage);
        }

        void ISubject_NoReply.Unsubscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer)
        {
            var requestMessage = new RequestMessage {
                InvokePayload = new ISubject_PayloadTable.Unsubscribe_Invoke { observer = (SubjectObserver)observer }
            };
            SendRequest(requestMessage);
        }
    }

    [AlternativeInterface(typeof(ISubject))]
    public interface ISubjectSync : IInterfacedActorSync
    {
        void MakeEvent(System.String eventName);
        void Subscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer);
        void Unsubscribe(Akka.Interfaced.SlimServer.ISubjectObserver observer);
    }
}

#endregion
#region Akka.Interfaced.SlimServer.ISubjectObserver

namespace Akka.Interfaced.SlimServer
{
    [PayloadTable(typeof(ISubjectObserver), PayloadTableKind.Notification)]
    public static class ISubjectObserver_PayloadTable
    {
        public static Type[] GetPayloadTypes()
        {
            return new Type[] {
                typeof(Event_Invoke),
            };
        }

        public class Event_Invoke : IInterfacedPayload, IInvokable
        {
            public System.String eventName;

            public Type GetInterfaceType()
            {
                return typeof(ISubjectObserver);
            }

            public void Invoke(object __target)
            {
                ((ISubjectObserver)__target).Event(eventName);
            }
        }
    }

    public class SubjectObserver : InterfacedObserver, ISubjectObserver
    {
        public SubjectObserver()
            : base(null, 0)
        {
        }

        public SubjectObserver(INotificationChannel channel, int observerId = 0)
            : base(channel, observerId)
        {
        }

        public void Event(System.String eventName)
        {
            var payload = new ISubjectObserver_PayloadTable.Event_Invoke { eventName = eventName };
            Notify(payload);
        }
    }

    [AlternativeInterface(typeof(ISubjectObserver))]
    public interface ISubjectObserverAsync : IInterfacedObserverSync
    {
        Task Event(System.String eventName);
    }
}

#endregion