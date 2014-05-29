using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Localization;
using Ninject.Modules;

namespace Bieb.Web.Infrastructure.NinjectModules
{
    public class LocalizationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIsbnLanguageDisplayer>().To<IsbnLanguageDisplayer>();
        }
    }
}