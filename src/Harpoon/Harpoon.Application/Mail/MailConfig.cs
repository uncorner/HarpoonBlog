using Harpoon.Core;

namespace Harpoon.Application.Mail
{
    public class MailConfig
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public bool EnableSsl { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public MailConfig(string host, int port, bool enableSsl, string userName, string password, string email)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("host", host);
            ArgumentHelper.EnsureNotNullOrEmpty("userName", userName);
            ArgumentHelper.EnsureNotNullOrEmpty("password", password);
            ArgumentHelper.EnsureNotNullOrEmpty("email", email);

            Host = host;
            Port = port;
            EnableSsl = enableSsl;
            UserName = userName;
            Password = password;
            Email = email;
        }

    }
}