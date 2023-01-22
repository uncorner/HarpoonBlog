using System.Collections.Generic;
using System.Web.Mvc;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Backend.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpPost]
        public JsonResult GetAvailible(IList<string> excludedTags, string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                return Json(new string[] {});
            }

            var processedPattern = pattern.Trim();
            var processedExcludedTags = excludedTags ?? new List<string>();
            
            var tags = tagRepository.FetchAllActualByPattern(processedExcludedTags, processedPattern);
            return Json(tags);
        }
        
    }
}