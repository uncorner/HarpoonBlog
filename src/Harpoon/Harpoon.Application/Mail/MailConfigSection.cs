using System.Configuration;

namespace Harpoon.Application.Mail
{
    public class MailConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["Host"]; }
        }

        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["Port"]; }
        }

        [ConfigurationProperty("EnableSsl", IsRequired = true)]
        public bool EnableSsl
        {
            get { return (bool)this["EnableSsl"]; }
        }

        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["UserName"]; }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
        }

        [ConfigurationProperty("Email", IsRequired = true)]
        public string Email
        {
            get { return (string)this["Email"]; }
        }

        public MailConfig GetConfig()
        {
            return new MailConfig(Host, Port, EnableSsl, UserName, Password, Email);
        }
        
    }
}