using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bieb.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon\.ico(/.*)?" });

            routes.MapRoute(
                "Account management", // Route name
                "Account", // URL with parameters
                new { controller = "Account", action = "Manage" } // Parameter defaults
            );

            routes.MapRoute("SearchIndex",
                            "Search",
                            new { controller = "Search", action = "Basic" });

            routes.MapRoute(
                "Details for a Book by ID", // Route name
                "Books/{id}", // URL with parameters
                new { controller = "Books", action = "Details", id = UrlParameter.Optional }, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Details for any item by ID", // Route name
                "{controller}/{id}", // URL with parameters
                new { controller = "Home", action = "Details", id = UrlParameter.Optional }, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Books action on item route", // Route name
                "Books/{id}/{action}", // URL with parameters
                new { controller = "Books", action = "Details" }, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Generic action on item route", // Route name
                "{controller}/{id}/{action}", // URL with parameters
                null, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Actions for Books controller via 'Books'", // Route name
                "Books/{action}", // URL with parameters
                new { controller = "Books", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Generic Controller and Action route", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );
        }
    }
}