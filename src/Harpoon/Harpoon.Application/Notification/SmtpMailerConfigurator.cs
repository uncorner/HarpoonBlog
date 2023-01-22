using System.Web.Configuration;
using Harpoon.Application.Mail;

namespace Harpoon.Application.Notification
{
    public static class SmtpMailerConfigurator
    {
        private readonly static MailConfig config;

        static SmtpMailerConfigurator()
        {
            var section = (MailConfigSection)WebConfigurationManager.GetSection("MailConfig");
            config = section.GetConfig();
        }

        public static SmtpMailer GetMailer()
        {
            return new SmtpMailer(config);
        }

    }
}