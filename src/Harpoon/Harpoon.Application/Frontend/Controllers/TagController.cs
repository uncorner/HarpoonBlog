using System.Web.Mvc;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public ActionResult List()
        {
            var tags = tagRepository.FetchAllActual();
            return View(tags);
        }

    }
}
