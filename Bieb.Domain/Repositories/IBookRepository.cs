using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;

namespace Bieb.Domain.Repositories
{
    public interface IBookRepository : IEntityRepository<Book>
    {
        /// <summary>
        /// Returns a list of integers representing the available ISBN Languages for this repository.
        /// </summary>
        IQueryable<int> IsbnLanguages { get; }
    }
}
