using System.Web.Mvc;
using Harpoon.Application.ViewHelpers;
using Harpoon.Application.ViewHelpers.Rendering;
using Harpoon.Application.ViewModels.Navigation;
using Harpoon.Core;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.ViewHelpers
{
    public static class TagMenuHelper
    {
        private const int MAX_ITEM_COUNT = 5;

        public static MvcHtmlString TagMenu(this HtmlHelper htmlHelper)
        {
            var unitOfWorkFactory = DependencyResolver.Current.GetService<IUnitOfWorkFactory>();
            var tagRepository = DependencyResolver.Current.GetService<ITagRepository>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var tags = tagRepository.FetchAllActual(MAX_ITEM_COUNT);
                var tagCount = tagRepository.GetActualCount();

                var footerLink = new MenuFooterLink("все теги", "Tag", "List");
                var menu = new SideMenu("tag-menu", "Теги", footerLink, tagCount);

                foreach (var tag in tags)
                {
                    menu.AddItem(new MenuItem(tag.Name, "Article", "ShowTaggedList", tag.Id));
                }

                menu.SelectItem(htmlHelper.GetCurrentRoute());

                var html = new SideMenuRenderer(htmlHelper, menu).Render();
                return new MvcHtmlString(html);
            }
        }
        
    }
}