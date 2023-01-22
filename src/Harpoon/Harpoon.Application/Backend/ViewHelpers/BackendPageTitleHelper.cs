using System.Web.Mvc;
using Harpoon.Application.ViewHelpers;

namespace Harpoon.Application.Backend.ViewHelpers
{
    public static class BackendPageTitleHelper
    {
        public static MvcHtmlString BackendPageTitle(this HtmlHelper htmlHelper)
        {
            return htmlHelper.PageTitle("Панель управления");
        }
        
    }
}