using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Framework.Logging;
using Ninject;
using Ninject.Web.Common;

namespace Bieb.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        private ILogger Logger
        {
            get
            {
                // Using Service Locator here because there seems to be no way to inject an ILogger with Ninject here
                return DependencyResolver.Current.GetService<ILogger>();
            }
        }
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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

        protected override Ninject.IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
        
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Logger.LogInformation("Application started.");
        }

        protected override void OnApplicationStopped()
        {
            base.OnApplicationStopped();

            Logger.LogInformation("Application stopped.");
        }

        protected void Application_EndRequest(object sender, EventArgs args)
        {
            // TODO: Refactor/replace with DI solution
            NHibernateProvider.Session.CloseSession();
        }
    }
}