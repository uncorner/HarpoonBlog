using System.Web.Mvc;
using Harpoon.Core;

namespace Harpoon.Application.ViewHelpers
{
    public static class PageTitleHelper
    {
        public static MvcHtmlString PageTitle(this HtmlHelper htmlHelper, string genericTitle)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("genericTitle", genericTitle);

            var viewTitle = htmlHelper.ViewData["Title"] as string;
            if (!string.IsNullOrEmpty(viewTitle))
            {
                return new MvcHtmlString(string.Format("{0} | {1}", viewTitle, genericTitle));
            }

            return new MvcHtmlString(genericTitle);
        }

    }
}