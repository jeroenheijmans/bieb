using System;
using System.Linq;
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
        private static ISession CurrentSession
        {
            get
            {
                return Session.Instance;
            }
        }

        public T GetItem(int id)
        {
            if (id == 0) throw new ArgumentException("GetItem of id 0 will not yield any results.");

            return CurrentSession.Get<T>(id);
        }

        public T GetRandomItem()
        {
            return CurrentSession
                    .CreateCriteria<T>()
                    .AddOrder(new RandomOrder())
                    .SetMaxResults(1)
                    .UniqueResult<T>();
        }

        public IQueryable<T> Items
        {
            get { return CurrentSession.Query<T>(); }
        }

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = CurrentSession.BeginTransaction())
            {
                CurrentSession.Save(item);
                transaction.Commit();
            }
        }

        public void Remove(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = CurrentSession.BeginTransaction())
            {
                CurrentSession.Delete(item);
                transaction.Commit();
            }
        }

        public void NotifyItemWasChanged(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = CurrentSession.BeginTransaction())
            {
                CurrentSession.Update(item);
                transaction.Commit();
            }
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
