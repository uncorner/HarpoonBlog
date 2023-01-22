using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Harpoon.Core;

namespace Harpoon.Application.Mail
{
    public class SmtpMailer
    {
        private readonly MailConfig config;

        public SmtpMailer(MailConfig config)
        {
            ArgumentHelper.EnsureNotNull("config", config);
            this.config = config;
        }

        public void Send(string subject, string messageBody, IEnumerable<string> recipientEmails)
        {
            ArgumentHelper.EnsureNotNull("config", config);
            ArgumentHelper.EnsureNotNullOrEmpty("subject", subject);
            ArgumentHelper.EnsureNotNullOrEmpty("messageBody", messageBody);
            ArgumentHelper.EnsureNotNull("recipientEmails", recipientEmails);

            if (new List<string>(recipientEmails).Count == 0)
            {
                throw new ApplicationException("recipientEmails is empty");
            }

            var mailMessage = new MailMessage
                              {
                                  From = new MailAddress(config.Email),
                                  Subject = subject,
                                  Body = messageBody,
                                  IsBodyHtml = true
                              };

            foreach (var recipientEmail in recipientEmails)
            {
                mailMessage.To.Add(recipientEmail);
            }

            var smtpClient = new SmtpClient(config.Host, config.Port)
            {
                EnableSsl = config.EnableSsl,
                Credentials = new NetworkCredential(config.UserName, config.Password)
            };

            smtpClient.Send(mailMessage);
        }

        public void Send(string subject, string messageBody, string recipientEmail)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("recipientEmail", recipientEmail);

            Send(subject, messageBody, new List<string> { recipientEmail });
        }

    }
}