using System.Web.Mvc;

namespace Harpoon.Web.Areas.Frontend
{
    public class FrontendAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Frontend";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
        }
    }
}
