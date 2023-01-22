using System;

namespace Harpoon.Application.Notification
{
    public interface IErrorNotificationSender
    {
        void Send(Exception exception);
    }
}