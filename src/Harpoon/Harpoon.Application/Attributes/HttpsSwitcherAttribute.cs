using System;
using System.Web.Mvc;

namespace Harpoon.Application.Attributes
{
    public class HttpsSwitcherAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var area = (string)filterContext.RouteData.DataTokens["area"];
            var request = filterContext.HttpContext.Request;

            if (!request.IsSecureConnection && (request.IsAuthenticated || area == RouteConfigurator.BACKEND_AREA))
            {
                // switch to https
                base.OnAuthorization(filterContext);
                return;
            }
            
            if (request.IsSecureConnection && !request.IsAuthenticated && area != RouteConfigurator.BACKEND_AREA)
            {
                SwitchToHttp(filterContext);
            }
        }

        private void SwitchToHttp(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.Url == null)
            {
                return;
            }

            var uriBuilder = new UriBuilder(request.Url)
                                 {
                                     Scheme = "http",
                                     Port = 80
                                 };

            filterContext.Result = new RedirectResult(uriBuilder.Uri.AbsoluteUri);
        }
    }
}