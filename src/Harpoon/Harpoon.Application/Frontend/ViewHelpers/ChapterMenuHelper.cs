using System.Web.Mvc;
using Harpoon.Application.ViewHelpers;
using Harpoon.Application.ViewHelpers.Rendering;
using Harpoon.Application.ViewModels.Navigation;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.ViewHelpers
{
    public static class ChapterMenuHelper
    {
        public static MvcHtmlString ChapterMenu(this HtmlHelper htmlHelper)
        {
            var menu = new Menu("layout-topbar-menu");
            menu.AddItem(new MenuItem("Заметки", "Article", "List"));

            var chapterRepository = DependencyResolver.Current.GetService<IChapterRepository>();
            var chapters = chapterRepository.FetchPublishedChapters();

            foreach (var chapter in chapters)
            {
                menu.AddItem(
                    new MenuItem(chapter.Title, "Chapter", "Show", chapter.TagName));
            }

            menu.SelectItem(htmlHelper.GetCurrentRoute());

            var html = new MenuRenderer(htmlHelper, menu).Render();
            return new MvcHtmlString(html);
        }
        
    }
}