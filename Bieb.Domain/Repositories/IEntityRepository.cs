using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Domain.Repositories
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        T GetItem(int id);
        T GetRandomItem();
        IQueryable<T> Items { get; }
        T Save(T item);
        void Delete(T item);
    }
}
