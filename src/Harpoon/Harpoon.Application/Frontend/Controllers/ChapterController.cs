using System.Web.Mvc;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.Controllers
{
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository chapterRepository;

        public ChapterController(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }

        [HttpGet]
        public ActionResult Show(string key)
        {
            CheckParameter("key", key);

            var chapter = chapterRepository.FetchByTagName(key);
            if (chapter == null)
            {
                throw new ContentNotFoundException("chapter", key);
            }
            
            return View(chapter);
        }

    }
}
