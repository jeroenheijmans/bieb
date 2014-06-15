using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Framework.Logging;
using Bieb.Web.Infrastructure.Filters;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace Bieb.Web.Infrastructure.NinjectModules
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            // TODO: For this the Log4netLogger has to be public, but it may be better to make it internal to Bieb.Framework?
            this.Bind<ILogger>().To<Log4NetLogger>().InSingletonScope();

            this.BindFilter<LogExceptionFilter>(FilterScope.Controller, 0)
                .WithConstructorArgument("logLevel", Level.Error);

            this.BindFilter<LogActionFilter>(FilterScope.Controller, 1)
                .WithConstructorArgument("logLevel", Level.Information);
        }
    }
}