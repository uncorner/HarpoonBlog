using System.Web.Mvc;
using Harpoon.Application.Caching;

namespace Harpoon.Application.Frontend.ViewHelpers
{
    public static class FooterTextHelper
    {
        public static MvcHtmlString FooterText(this HtmlHelper htmlHelper)
        {
            var personalSetting = DependencyResolver.Current.GetService<ICacheBroker>()
                .GetPersonalSetting();

            var html = string.Format("<span>{0}</span>", personalSetting.FooterText);

            return new MvcHtmlString(html);
        }
        
    }
}