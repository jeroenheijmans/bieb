using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Repositories;
using Bieb.NHibernateProvider.Repositories;
using Ninject.Modules;

namespace Bieb.Web.Infrastructure
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IEntityRepository<>)).To(typeof(EntityRepository<>));
        }
    }
}