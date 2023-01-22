using System.Collections.Generic;

namespace Harpoon.Application.ViewModels.Navigation
{
    public class SideMenu : Menu
    {
        private readonly int totalItemCount;
        public string Title { get; private set; }
        public MenuFooterLink FooterLink { get; private set; }
        public bool IsShowFooterLink { get; private set; } 

        public SideMenu(string id, string title, MenuFooterLink footerLink, int totalItemCount) : base(id)
        {
            Title = title;
            FooterLink = footerLink;
            this.totalItemCount = totalItemCount;
        }

        public override void SelectItem(MenuRoute route)
        {
            base.SelectItem(route);

            var itemList = new List<MenuItem>(Items);
            IsShowFooterLink = itemList.Count < totalItemCount && ! FooterLink.Equals(route);
        }

    }
}