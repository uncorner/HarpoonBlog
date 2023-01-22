using System.Web.Mvc;
using System.Web.Routing;

namespace Harpoon.Application
{
    public static class RouteConfigurator
    {
        private const string FRONTEND_CONTROLLER_NAMESPACE = "Harpoon.Application.Frontend.Controllers";
        private const string BACKEND_CONTROLLER_NAMESPACE = "Harpoon.Application.Backend.Controllers";
        public const string FRONTEND_AREA = "Frontend";
        public const string BACKEND_AREA = "Backend";
        public const string AUTH_CONTROLLER = "Auth";

        public static void Configure(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Error",
                "error/{action}",
                new { controller = "Error" });

            // backend
            MapBackendRoute(
                routes,
                "admin",
                new { controller = "Article", action = "ShowArticles" },
                "AdminRoot"
                );

            MapBackendRoute(
                routes,
                "admin/chapters",
                new {controller = "Chapter", action = "ShowChapters"}
                );
                
            MapBackendRoute(
                routes,
                "admin/logon",
                new { controller = AUTH_CONTROLLER, action = "Logon" },
                "Logon"
                );

            MapBackendRoute(
                routes,
                "admin/logoff",
                new { controller = AUTH_CONTROLLER, action = "Logoff" }
                );

            MapBackendRoute(
                routes,
                "admin/editprofile",
                new { controller = "Profile", action = "Edit" }
                );
            
            MapBackendRoute(
               routes,
               "admin/{controller}/{action}/{id}",
               new { controller = "Article", action = "ShowArticles", id = UrlParameter.Optional }
               );

            // frontend
            MapFrontendRoute(
                routes,
                "",
                new { controller = "Article", action = "List" },
                "Root"
                );

            MapFrontendRoute(
                routes,
                "rss",
                new {controller = "Article", action = "Rss" },
                "Rss"
                );

            MapFrontendRoute(
                routes,
                "article/getlist",
                new { controller = "Article", action = "GetList", key = UrlParameter.Optional }
                );

            MapFrontendRoute(
                routes,
                "article/{key}",
                new { controller = "Article", action = "Show", key = UrlParameter.Optional }
                );

            MapFrontendRoute(
                routes,
                "tags",
                new {controller = "Tag", action = "List"}
                );
            
            MapFrontendRoute(
                routes,
                "tag/{key}",
                new { controller = "Article", action = "ShowTaggedList", key = UrlParameter.Optional }
                );

            MapFrontendRoute(
                routes,
                "search/{query}",
                new {controller = "Search", action = "Find", query = UrlParameter.Optional}
                );

            MapFrontendRoute(
                routes,
                "{key}",
                new { controller = "Chapter", action = "Show", key = UrlParameter.Optional }
                );

            MapFrontendRoute(
                routes,
                "{controller}/{action}/{id}",
                new {controller = "Article", action = "List", id = UrlParameter.Optional }
                );
        }
        
        private static void MapFrontendRoute(RouteCollection routes, string url, object defaults, string name = null)
        {
            routes.MapRoute(name, url, defaults,
                new[] { FRONTEND_CONTROLLER_NAMESPACE })
                .DataTokens.Add("area", FRONTEND_AREA);
        }

        private static void MapBackendRoute(RouteCollection routes, string url, object defaults, string name = null)
        {
            routes.MapRoute(name, url, defaults,
                new[] { BACKEND_CONTROLLER_NAMESPACE })
                .DataTokens.Add("area", BACKEND_AREA);
        }

    }
}
