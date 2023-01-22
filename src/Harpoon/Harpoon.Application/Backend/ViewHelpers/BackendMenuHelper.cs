using System.Web.Mvc;
using Harpoon.Application.ViewHelpers;
using Harpoon.Application.ViewHelpers.Rendering;
using Harpoon.Application.ViewModels.Navigation;

namespace Harpoon.Application.Backend.ViewHelpers
{
    public static class BackendMenuHelper
    {
        private static Menu backendMenu;

        private static Menu GetMenu()
        {
            if (backendMenu == null)
            {
                backendMenu = new Menu("sidebar-menu")
                    .AddItem(new MenuItem("Заметки", "Article", "ShowArticles"))
                    .AddItem(new MenuItem("Разделы", "Chapter", "ShowChapters"))
                    .AddItem(new MenuItem("Профиль", "Profile", "Edit"))
                    .AddItem(new MenuItem("Пароль", "Auth", "ChangePassword"));
            }

            return backendMenu;
        }
        
        public static MvcHtmlString BackendMenu(this HtmlHelper htmlHelper)
        {
            var menu = GetMenu();
            menu.SelectItem(htmlHelper.GetCurrentRoute());

            var html = new MenuRenderer(htmlHelper, backendMenu).Render();
            return new MvcHtmlString(html);
        }

    }
}