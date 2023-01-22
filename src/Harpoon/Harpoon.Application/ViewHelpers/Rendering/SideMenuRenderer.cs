using System.Web.Mvc;
using Harpoon.Application.ViewModels.Navigation;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public class SideMenuRenderer : ListMenuRenderer
    {
        public SideMenuRenderer(HtmlHelper htmlHelper, SideMenu menu) : base(htmlHelper, menu)
        {
        }

        protected override string RenderTopOfList()
        {
            var sideMenu = (SideMenu) menu;

            var html = string.Format(@"<div id=""{0}"" class=""sidebar-menu"">", sideMenu.Id);
            html += @"<div class=""sidebar-menu-topbar""></div>";
            html += @"<div class=""sidebar-menu-content"">";
            html += string.Format(@"<div class=""sidebar-menu-header""><span>{0}</span></div>", sideMenu.Title);
            html += @"<ul class=""tuned-ul"">";

            return html;
        }

        protected override string RenderBottomOfList()
        {
            var sideMenu = (SideMenu) menu;
            var html = "</ul>";

            if (sideMenu.IsShowFooterLink)
            {
                html += @"<div class=""sidebar-menu-footer"">";
                html += "<span>&#8249;</span>";
                var footerLinkHtml = RenderMenuLink(sideMenu.FooterLink.Title, sideMenu.FooterLink);
                html += string.Format(@"<span>&nbsp;{0}</span>", footerLinkHtml);
                html += "</div>";
            }

            html += "</div>";
            html += "</div>";

            return html;
        }

    }
}