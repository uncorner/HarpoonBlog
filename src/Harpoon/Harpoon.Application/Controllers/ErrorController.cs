using System.Net;
using System.Web.Mvc;

namespace Harpoon.Application.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound(string url)
        {
            ViewBag.Url = url;
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            SkipIisCustomErrors();

            return View();
        }
        

        public ActionResult Internal()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            SkipIisCustomErrors();

            return View();
        }

        private void SkipIisCustomErrors()
        {
            Response.TrySkipIisCustomErrors = true;
        }

    }
}
