using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Harpoon.Application.ViewModels.Navigation;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public abstract class ListMenuRenderer
    {
        protected readonly Menu menu;
        protected readonly HtmlHelper htmlHelper;
        
        protected ListMenuRenderer(HtmlHelper htmlHelper, Menu menu)
        {
            this.menu = menu;
            this.htmlHelper = htmlHelper;
        }

        protected abstract string RenderTopOfList();
        protected abstract string RenderBottomOfList();

        public string Render()
        {
            if (! menu.HasItems)
            {
                return string.Empty;
            }

            var html = RenderTopOfList();

            foreach (var item in menu.Items)
            {
                html += RenderMenuItem(item);
            }

            html += RenderBottomOfList();

            return html;
        }

        private string RenderMenuItem(MenuItem item)
        {
            var html = item.IsSelected ? string.Format(@"<li class=""selected-item"">{0}</li>", item.Title)
                              : string.Format("<li>{0}</li>", RenderMenuLink(item.Title, item));

            return html;
        }

        protected string RenderMenuLink(string title, MenuRoute route)
        {
            if (route.HasKey)
            {
                return htmlHelper.ActionLink(title, route.Action, route.Controller,
                                             new RouteValueDictionary(route.GetKeyData()), null)
                    .ToHtmlString();
            }

            return htmlHelper.ActionLink(title, route.Action, route.Controller)
                .ToHtmlString();
        }

    }
}