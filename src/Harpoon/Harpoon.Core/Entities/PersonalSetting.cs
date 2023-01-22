namespace Harpoon.Core.Entities
{
    public class PersonalSetting
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string FooterText { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Subject { get; set; }

        private PersonalSetting()
        {
        }

        public PersonalSetting(string title, string footerText, string email,
            string name, string passwordHash)
        {
            SetTitle(title);
            SetFooterText(footerText);
            SetName(name);
            SetEmail(email);
            SetPasswordHash(passwordHash);
        }

        public void SetTitle(string title)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("title", title);
            Title = title;
        }

        public void SetFooterText(string footerText)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("footerText", footerText);
            FooterText = footerText;
        }

        public void SetName(string name)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("name", name);
            Name = name;
        }

        public void SetEmail(string email)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("email", email);
            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("passwordHash", passwordHash);
            PasswordHash = passwordHash;
        }

        public bool HasSubject()
        {
            return !string.IsNullOrEmpty(Subject);
        }

    }

}