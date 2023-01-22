namespace Harpoon.Core.Entities
{
    public class GuestComment : Comment
    {
        private string email;

        public string Name { get; private set; }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = string.IsNullOrEmpty(value) ? value : value.Trim();
            }
        }

        private GuestComment()
        {
        }

        public GuestComment(string name, string content) : base(content)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("name", name);
            Name = name;
        }

    }
}