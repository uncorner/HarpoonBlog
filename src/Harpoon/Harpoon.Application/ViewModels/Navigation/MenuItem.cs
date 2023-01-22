namespace Harpoon.Application.ViewModels.Navigation
{
    public class MenuItem : MenuRoute
    {
        public string Title { get; private set; }
        public bool IsSelected { get; set; }

        public MenuItem(string title, string controller, string action, string key = null)
            : base(controller, action, key)
        {
            Title = title;
        }

    }
}