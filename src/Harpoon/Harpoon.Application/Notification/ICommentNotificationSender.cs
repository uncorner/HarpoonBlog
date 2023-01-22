using Harpoon.Core.Entities;

namespace Harpoon.Application.Notification
{
    public interface ICommentNotificationSender
    {
        void Send(GuestComment comment, Article article, string articleUrl);
    }
}