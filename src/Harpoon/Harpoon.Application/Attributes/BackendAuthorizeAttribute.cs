using System.Web.Mvc;

namespace Harpoon.Application.Attributes
{
    public class BackendAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var area = (string)filterContext.RouteData.DataTokens["area"];
            var controller = filterContext.RouteData.GetRequiredString("controller");

            if (area == RouteConfigurator.BACKEND_AREA && controller != RouteConfigurator.AUTH_CONTROLLER)
            {
                base.OnAuthorization(filterContext);
            }
        }
        
    }
}