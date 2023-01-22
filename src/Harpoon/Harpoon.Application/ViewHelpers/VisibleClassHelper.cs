using System.Web.Mvc;

namespace Harpoon.Application.ViewHelpers
{
    public static class VisibleClassHelper
    {
        public static MvcHtmlString VisibleClass(this HtmlHelper htmlHelper, bool isVisible)
        {
            var classString = isVisible ? string.Empty : "hidden";
            return new MvcHtmlString(classString);
        }
        
    }
}