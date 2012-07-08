using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using Bieb.Domain.Entities;

namespace Bieb.NHibernateProvider.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        private ISession session
        {
            get
            {
                return NHibernateProvider.Session.Instance;
            }
        }

        public T Get(int id)
        {
            return session.Load<T>(id);
        }

        public IQueryable<T> Items
        {
            get { return session.Query<T>(); }
        }

        public void Add(T item)
        {
            session.Save(item);
        }

        public void Delete(T item)
        {
            session.Delete(item);
        }
    }
}
