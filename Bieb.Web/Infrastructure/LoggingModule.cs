using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Framework.Logging;
using Ninject.Modules;

namespace Bieb.Web.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<NLogLogger>().InSingletonScope();
        }
    }
}