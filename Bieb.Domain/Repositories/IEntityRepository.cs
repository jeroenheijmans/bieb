using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;

namespace Bieb.Domain.Repositories
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IQueryable<T> Items { get; }
        T Save(T item);
        void Delete(T item);
    }
}
