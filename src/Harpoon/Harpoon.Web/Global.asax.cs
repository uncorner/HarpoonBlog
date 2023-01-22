using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Harpoon.Application;
using Harpoon.Application.Attributes;
using Harpoon.Application.ErrorHandlers;
using Harpoon.Application.Notification;
using Harpoon.Application.Searching;
using NLog;
using StackExchange.Profiling;

namespace Harpoon.Web
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IErrorContext
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private static readonly IErrorHandler errorHandler = ErrorHandlerFactory.Create();
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(), 0);
            filters.Add(HttpsAttributeFactory.Create(), 1);
            filters.Add(new BackendAuthorizeAttribute(), 2);
        }

        protected void Application_Start()
        {
            log.Info("Start application...");

            MiniProfilerEF.Initialize();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfigurator.Configure(RouteTable.Routes);

            var container = ContainerConfigurator.GetContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // rebuild searching index
            RebuildIndex();
            
            log.Info("Application started");
        }

        private void RebuildIndex()
        {
            var searchEngine = DependencyResolver.Current.GetService<ISearchEngine>();
            var thread = new Thread(searchEngine.RebuildIndex);
            thread.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            log.Info("Stop application");
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            errorHandler.Handle(this);
        }

        #region Implementation of IErrorContext

        public Exception GetException()
        {
            return Server.GetLastError();
        }

        public void Redirect(IDictionary<string, object> routeValues)
        {
            Response.RedirectToRoute(new RouteValueDictionary(routeValues));
        }

        public void ClearError()
        {
            Server.ClearError();
        }

        public string GetRequestedUrl()
        {
            return Request.RawUrl;
        }

        public IErrorNotificationSender GetSender()
        {
            return new ErrorMailSender();
        }

        #endregion
    }
}