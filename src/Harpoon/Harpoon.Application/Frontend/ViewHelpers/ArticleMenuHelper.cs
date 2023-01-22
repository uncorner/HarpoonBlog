using System.Web.Mvc;
using Harpoon.Application.ViewHelpers;
using Harpoon.Application.ViewHelpers.Rendering;
using Harpoon.Application.ViewModels.Navigation;
using Harpoon.Core;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.ViewHelpers
{
    public static class ArticleMenuHelper
    {
        private const int MAX_ITEM_COUNT = 5;

        public static MvcHtmlString ArticleMenu(this HtmlHelper htmlHelper)
        {
            var unitOfWorkFactory = DependencyResolver.Current.GetService<IUnitOfWorkFactory>();
            var articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var articles = articleRepository.FetchAllPublished(MAX_ITEM_COUNT);
                var articleCount = articleRepository.GetPublishedCount();

                var footerLink = new MenuFooterLink("все заметки", "Article", "List");
                var menu = new SideMenu("article-menu", "Свежие заметки", footerLink, articleCount);

                foreach (var article in articles)
                {
                    menu.AddItem(new MenuItem(article.Title, "Article", "Show", article.Id.ToString()));
                }

                menu.SelectItem(htmlHelper.GetCurrentRoute());

                var html = new SideMenuRenderer(htmlHelper, menu).Render();
                return new MvcHtmlString(html);
            }
        }

    }
}