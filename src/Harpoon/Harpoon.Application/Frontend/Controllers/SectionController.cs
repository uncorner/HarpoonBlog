using System.Web.Mvc;
using Harpoon.Application.Caching;

namespace Harpoon.Application.Frontend.Controllers
{
    public class SectionController : Controller
    {
        private readonly ICacheBroker cacheBroker;

        public SectionController(ICacheBroker cacheBroker)
        {
            this.cacheBroker = cacheBroker;
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult ShowLayoutHeader()
        {
            var personalSetting = cacheBroker.GetPersonalSetting();
            return PartialView("LayoutHeader", personalSetting);
        }

    }
}
