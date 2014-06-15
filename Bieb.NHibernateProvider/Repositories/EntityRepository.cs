using System;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;

namespace Bieb.DataAccess.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        public EntityRepository(ISessionProvider sessionProvider)
        {
            currentSession = sessionProvider.Current;
        }

        private readonly ISession currentSession;

        public T GetItem(int id)
        {
            return currentSession.Get<T>(id);
        }

        public T GetRandomItem()
        {
            return currentSession
                    .CreateCriteria<T>()
                    .AddOrder(new RandomOrder())
                    .SetMaxResults(1)
                    .UniqueResult<T>();
        }

        public IQueryable<T> Items
        {
            get { return currentSession.Query<T>(); }
        }

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = currentSession.BeginTransaction())
            {
                currentSession.Save(item);
                transaction.Commit();
            }
        }

        public void Remove(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = currentSession.BeginTransaction())
            {
                currentSession.Delete(item);
                transaction.Commit();
            }
        }

        public void NotifyItemWasChanged(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            using (var transaction = currentSession.BeginTransaction())
            {
                currentSession.Update(item);
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
