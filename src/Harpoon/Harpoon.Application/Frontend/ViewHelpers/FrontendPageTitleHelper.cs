using System.Web.Mvc;
using Harpoon.Application.Caching;
using Harpoon.Application.ViewHelpers;

namespace Harpoon.Application.Frontend.ViewHelpers
{
    public static class FrontendPageTitleHelper
    {
        public static MvcHtmlString FrontendPageTitle(this HtmlHelper htmlHelper)
        {
            var personalSetting = DependencyResolver.Current.GetService<ICacheBroker>()
                .GetPersonalSetting();
            
            return htmlHelper.PageTitle(personalSetting.Title);
        }
        
    }
}