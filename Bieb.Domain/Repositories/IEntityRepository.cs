using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Domain.Repositories
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Retrieves an item by its ID from the repository
        /// </summary>
        T GetItem(int id);

        /// <summary>
        /// Returns one random item from the repository
        /// </summary>
        /// <returns></returns>
        T GetRandomItem();

        /// <summary>
        /// All items in the repository
        /// </summary>
        IQueryable<T> Items { get; }

        /// <summary>
        /// Add a new item to the repository
        /// </summary>
        /// <returns>Reference to the updated item</returns>
        void Add(T item);

        /// <summary>
        /// Removes an item from the repository
        /// </summary>
        void Remove(T item);

        /// <summary>
        /// Notifies the repository that the item was changed and should possibly be saved.
        /// </summary>
        /// TODO: This shouldn't be here because it's not a repository's responsibility to save updated items. Possibly another interface, or...?
        void NotifyItemWasChanged(T item);
    }
}
