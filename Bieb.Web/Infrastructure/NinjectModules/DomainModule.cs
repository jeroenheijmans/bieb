using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Repositories;
using Bieb.NHibernateProvider.Repositories;
using Ninject.Modules;

namespace Bieb.Web.Infrastructure
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookRepository>().To<BookRepository>();
            Bind(typeof(IEntityRepository<>)).To(typeof(EntityRepository<>));
        }
    }
}