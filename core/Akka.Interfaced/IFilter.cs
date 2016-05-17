﻿using System;
using System.Threading.Tasks;

namespace Akka.Interfaced
{
    public interface IFilter
    {
        int Order { get; }
    }

    // Pre-Request Filter

    public class PreRequestFilterContext
    {
        public object Actor;
        public RequestMessage Request;
        public ResponseMessage Response;
    }

    public interface IPreRequestFilter : IFilter
    {
        void OnPreRequest(PreRequestFilterContext context);
    }

    public interface IPreRequestAsyncFilter : IFilter
    {
        Task OnPreRequestAsync(PreRequestFilterContext context);
    }

    // Post-Request Filter

    public class PostRequestFilterContext
    {
        public object Actor;
        public RequestMessage Request;
        public ResponseMessage Response;
    }

    public interface IPostRequestFilter : IFilter
    {
        void OnPostRequest(PostRequestFilterContext context);
    }

    public interface IPostRequestAsyncFilter : IFilter
    {
        Task OnPostRequestAsync(PostRequestFilterContext context);
    }

    // Pre-Notification Filter

    public class PreNotificationFilterContext
    {
        public object Actor;
        public NotificationMessage Notification;
    }

    public interface IPreNotificationFilter : IFilter
    {
        void OnPreNotification(PreNotificationFilterContext context);
    }

    public interface IPreNotificationAsyncFilter : IFilter
    {
        Task OnPreNotificationAsync(PreNotificationFilterContext context);
    }

    // Post-Notification Filter

    public class PostNotificationFilterContext
    {
        public object Actor;
        public NotificationMessage Notification;
    }

    public interface IPostNotificationFilter : IFilter
    {
        void OnPostNotification(PostNotificationFilterContext context);
    }

    public interface IPostNotificationAsyncFilter : IFilter
    {
        Task OnPostNotificationAsync(PostNotificationFilterContext context);
    }

    // Pre-Message Filter
    // Post-Message Filter
}
