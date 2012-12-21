using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Web.Infrastructure;

namespace Bieb.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<string, string> ControllerAliases = new Dictionary<string, string>();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon\.ico(/.*)?" });

            routes.MapRoute("SearchIndex",
                            "Search",
                            new { controller = "Search", action = "Basic" });

            routes.MapRoute(
                "Details for a LibraryBook by ID", // Route name
                "Books/{id}", // URL with parameters
                new { controller = "LibraryBooks", action = "Details", id = UrlParameter.Optional }, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Details for any item by ID", // Route name
                "{controller}/{id}", // URL with parameters
                new { controller = "Home", action = "Details", id = UrlParameter.Optional }, // Parameter defaults
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Actions for LibraryBooks controller via 'Books'", // Route name
                "Books/{action}", // URL with parameters
                new { controller = "LibraryBooks", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Generic Controller and Action route", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        protected void Application_EndRequest(object sender, EventArgs args)
        {
            // TODO: Refactor/replace with DI solution
            NHibernateProvider.Session.CloseSession();
        }
    }
}