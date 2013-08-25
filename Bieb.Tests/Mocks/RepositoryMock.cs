using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Tests.Mocks
{
    public class RepositoryMock<T> : IEntityRepository<T> where T : BaseEntity
    {
        private readonly IList<T> items;

        public RepositoryMock()
        {
            items = new List<T>();
        }

        public RepositoryMock(IList<T> items)
        {
            this.items = items;
        }

        public T GetItem(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public T GetRandomItem()
        {
            return items.FirstOrDefault();
        }

        public IQueryable<T> Items
        {
            get { return items.AsQueryable(); }
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public void NotifyItemWasChanged(T item)
        {
            throw new NotImplementedException();
        }
    }
}
