using System;
using System.Collections.Generic;
using System.Web;
using Harpoon.Core;
using NLog;

namespace Harpoon.Application.ErrorHandlers
{
    public class ErrorHandler : IErrorHandler
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
         
        public void Handle(IErrorContext context)
        {
            ArgumentHelper.EnsureNotNull("context", context);
             
            var exception = context.GetException();
            if (exception == null)
            {
                return;
            }

            context.ClearError();
            var httpException = exception as HttpException;

            if (httpException != null && httpException.GetHttpCode() == 404
                || exception is ActionParameterNullException || exception is ContentNotFoundException)
            {
                var requestedUrl = context.GetRequestedUrl();
                log.Warn("Resource is not found: {0}; {1}: {2}", requestedUrl,
                    exception.GetType().FullName, exception.Message);

                var routeValues = BuildRouteValues("Error", "NotFound");
                routeValues.Add("url", requestedUrl);

                context.Redirect(routeValues);
                return;
            }

            // NOTE: unhandled exception
            log.Fatal(exception.ToString());

            try
            {
                // send message
                context.GetSender().Send(exception);
            }
            catch(Exception ex)
            {
                log.Error("Не удалось отправить уведомление об ошибке: " + ex);
            }

            context.Redirect(BuildRouteValues("Error", "Internal"));
        }

        private IDictionary<string, object> BuildRouteValues(string controller, string action)
        {
            var values = new Dictionary<string, object>
                             {
                                 {"controller", controller}, {"action", action}
                             };
            return values;
        }

    }
}