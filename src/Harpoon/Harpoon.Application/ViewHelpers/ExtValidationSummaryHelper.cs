using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Harpoon.Application.ViewHelpers
{
    public static class ExtValidationSummaryHelper
    {
        public static MvcHtmlString ExtValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            var html = htmlHelper.ValidationSummary(excludePropertyErrors);
            return MvcHtmlString.IsNullOrEmpty(html) || html.ToString().Contains("display:none")
                ? MvcHtmlString.Empty : html;
        }

        public static MvcHtmlString ExtValidationSummary(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ExtValidationSummary(true);
        }
    }
}