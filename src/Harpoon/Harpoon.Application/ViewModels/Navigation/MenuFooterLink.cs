namespace Harpoon.Application.ViewModels.Navigation
{
    public class MenuFooterLink : MenuRoute
    {
        public string Title { get; private set; }

        public MenuFooterLink(string title, string controller, string action)
            : base(controller, action)
        {
            Title = title;
        }

    }
}