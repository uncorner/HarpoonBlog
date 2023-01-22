using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Harpoon.Application.Attributes
{
    public static class HttpsAttributeFactory
    {
        public static FilterAttribute Create()
        {
            var enable = Convert.ToBoolean(WebConfigurationManager.AppSettings["HttpsEnable"]);

            if (enable)
            {
                return new HttpsSwitcherAttribute();
            }

            return new EmptyFilterAttribute();
        }
    }
}
