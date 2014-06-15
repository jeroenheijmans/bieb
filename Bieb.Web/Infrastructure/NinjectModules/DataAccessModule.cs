using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Repositories;
using Bieb.NHibernateProvider;
using Bieb.NHibernateProvider.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Bieb.Web.Infrastructure.NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFactoryProvider>().To<FactoryProvider>().InSingletonScope();
            Bind<ISessionProvider>().To<SessionProvider>().InRequestScope();
            Bind<IBookRepository>().To<BookRepository>();
            Bind(typeof(IEntityRepository<>)).To(typeof(EntityRepository<>));
        }
    }
}