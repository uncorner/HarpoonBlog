using System.Web.Mvc;
using Harpoon.Application.Frontend.ViewModels;
using Harpoon.Application.Searching;

namespace Harpoon.Application.Frontend.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchEngine searchEngine;

        public SearchController(ISearchEngine searchEngine)
        {
            this.searchEngine = searchEngine;
        }

        public ActionResult Find(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToRoute("Root");
            }
            query = query.Trim();

            var articles = searchEngine.Find(query);
            return View(new SearchForm(query, articles));
        }
        
    }
}