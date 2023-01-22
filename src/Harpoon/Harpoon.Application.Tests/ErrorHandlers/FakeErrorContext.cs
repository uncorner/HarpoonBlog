using System;
using System.Collections.Generic;
using Harpoon.Application.Notification;
using NUnit.Framework;

namespace Harpoon.Application.ErrorHandlers
{
    public class FakeErrorContext : IErrorContext, IErrorNotificationSender
    {
        private readonly Exception exception;
        private IDictionary<string, object> routeValues;
        public string RequestedUrl { get; set; }
        public bool IsClearError { get; private set; }
        public bool IsErrorSent { get; private set; }

        public FakeErrorContext(Exception exception)
        {
            this.exception = exception;
        }

        public Exception GetException()
        {
            return exception;
        }

        public void Redirect(IDictionary<string, object> routeValues)
        {
            Assert.IsNotNull(routeValues);
            this.routeValues = routeValues;
        }
        
        public void ClearError()
        {
            IsClearError = true;
        }

        public string GetRequestedUrl()
        {
            return RequestedUrl;
        }

        public IErrorNotificationSender GetSender()
        {
            return this;
        }

        public bool IsRedirected()
        {
            return routeValues != null;
        }

        public void AssertRouteController(string name)
        {
            AssertRouteParam("controller", name);
        }

        public void AssertRouteAction(string name)
        {
            AssertRouteParam("action", name);
        }

        public void AssertRouteParam(string name, string value)
        {
            Assert.NotNull(routeValues);
            Assert.IsTrue(routeValues.ContainsKey(name));
            Assert.AreEqual(value.ToLowerInvariant(), ((string)routeValues[name]).ToLowerInvariant());
        }

        #region Implementation of IErrorNotificationSender

        public void Send(Exception exception)
        {
            IsErrorSent = true;
        }

        #endregion
    }
}