using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using Bieb.Domain.Entities;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Bieb.NHibernateProvider.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        // This is static... for now, as long as it's merely referring to the static singleton anyhow
        private static ISession session
        {
            get
            {
                return NHibernateProvider.Session.Instance;
            }
        }

        public T GetItem(int id)
        {
            return session.Load<T>(id);
        }

        public T GetRandomItem()
        {
            return session
                    .CreateCriteria<T>()
                    .AddOrder(new RandomOrder())
                    .SetMaxResults(1)
                    .UniqueResult<T>();
        }

        public IQueryable<T> Items
        {
            get { return session.Query<T>(); }
        }

        public T Save(T item)
        {
            session.Save(item);
            // Save returns the persisted ID (which will be Int32 for Bieb.Domain entities)
            // So go ahead and just return the persisted item.
            return item;
        }

        public void Delete(T item)
        {
            session.Delete(item);
        }

        private class RandomOrder : Order
        {
            public RandomOrder() : base("", true) { }
            public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
            {
                return new SqlString("NEWID()");
            }
        }
    }
}
