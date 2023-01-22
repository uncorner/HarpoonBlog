using System;
using System.Web.Configuration;

namespace Harpoon.Application.ErrorHandlers
{
    public static class ErrorHandlerFactory
    {
        public static IErrorHandler Create()
        {
            var enable = Convert.ToBoolean(WebConfigurationManager.AppSettings["ErrorHandlerEnable"]);

            if (enable)
            {
                return new ErrorHandler();
            }

            return new NullErrorHandler();
        }
        
    }
}