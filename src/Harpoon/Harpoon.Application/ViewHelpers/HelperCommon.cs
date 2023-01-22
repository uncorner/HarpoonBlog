using System.Web.Mvc;
using Harpoon.Application.ViewModels.Navigation;

namespace Harpoon.Application.ViewHelpers
{
    internal static class HelperCommon
    {
        private const string KEY_NAME = "key";

        public static MenuRoute GetCurrentRoute(this HtmlHelper htmlHelper)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");

            var key = routeData.Values.ContainsKey(KEY_NAME)
                          ? (string) routeData.Values[KEY_NAME]
                          : htmlHelper.ViewContext.HttpContext.Request.QueryString[KEY_NAME];

            return new MenuRoute(controller, action, key);
        }
    }
}