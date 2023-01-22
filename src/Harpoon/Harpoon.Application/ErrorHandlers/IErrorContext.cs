using System;
using System.Collections.Generic;
using Harpoon.Application.Notification;

namespace Harpoon.Application.ErrorHandlers
{
    public interface IErrorContext
    {
        Exception GetException();
        void Redirect(IDictionary<string, object> routeValues);
        void ClearError();
        string GetRequestedUrl();
        IErrorNotificationSender GetSender();
    }
}