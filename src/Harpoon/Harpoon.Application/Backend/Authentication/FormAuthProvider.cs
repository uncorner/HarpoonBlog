using System;
using System.Web.Security;
using Harpoon.Core;

namespace Harpoon.Application.Backend.Authentication
{
    public class FormAuthProvider : IAuthProvider
    {
        private const string USER_NAME = "master";
        
        public bool TrySignIn(string passwordHash, string password)
        {
            if (CheckPassword(passwordHash, password))
            {
                FormsAuthentication.SetAuthCookie(USER_NAME, true);
                return true;
            }

            return false;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
        
        public bool CheckPassword(string passwordHash, string password)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("passwordHash", passwordHash);
            ArgumentHelper.EnsureNotNullOrEmpty("password", password);

            var calculatedHash = GetPasswordHash(password);
            return calculatedHash == passwordHash;
        }

        public string GetPasswordHash(string password)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("password", password);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
        }

    }
}