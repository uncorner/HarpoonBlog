using System.Web.Mvc;
using System.Collections.Generic;
using ControllerBase = Harpoon.Application.ControllerBase;

namespace Harpoon.Application.ViewHelpers
{
    public static class FlashMessageHelper
    {
        public static MvcHtmlString FlashMessage(this HtmlHelper htmlHelper)
        {
            var messages = (IList<string>)htmlHelper.ViewContext.TempData[ControllerBase.FLASH_MESSAGE_KEY];
            if (messages == null || messages.Count == 0)
            {
                return new MvcHtmlString("");
            }

            var html = @"<ul class=""flash-message"">";

            foreach (var message in messages)
            {
                html += string.Format("<li>{0}</li>", message);
            }

            html += "</ul>";

            return new MvcHtmlString(html);
        }
        
    }
}