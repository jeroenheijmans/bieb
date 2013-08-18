using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bieb.Framework.Logging;
using Bieb.Web.App_Start;
using Ninject;
using Ninject.Web.Common;
using WebMatrix.WebData;

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

        protected override Ninject.IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        protected void Application_Error(Object sender, EventArgs eventArgs)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Logger.LogError(ex);
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            WebSecurity.InitializeDatabaseConnection("BiebDatabase", "UserProfile", "UserId", "UserName", autoCreateTables: true);

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