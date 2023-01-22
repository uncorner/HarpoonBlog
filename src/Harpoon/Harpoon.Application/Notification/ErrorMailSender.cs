using System;
using System.Web.Configuration;
using Harpoon.Core;

namespace Harpoon.Application.Notification
{
    public class ErrorMailSender : IErrorNotificationSender
    {
        private static string notificationEmail;

        public void Send(Exception exception)
        {
            ArgumentHelper.EnsureNotNull("exception", exception);

            var email = GetNotificationEmail();
            if (string.IsNullOrEmpty(email))
            {
                return;
            }

            var mailer = SmtpMailerConfigurator.GetMailer();
            mailer.Send("Сообщение об ошибке", exception.ToString(), email);
        }

        private string GetNotificationEmail()
        {
            if (string.IsNullOrEmpty(notificationEmail))
            {
                notificationEmail = WebConfigurationManager.AppSettings["ErrorNotificationEmail"];
            }

            return notificationEmail;
        }
        
    }
}