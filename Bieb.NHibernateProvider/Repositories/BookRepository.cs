using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.NHibernateProvider.Repositories
{
    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public IQueryable<int> IsbnLanguages
        {
            get
            {
                // TODO: This should come from another source
                return new[]
                           {
                               0, // English
                               1, // English
                               2, // French
                               3, // German
                               4, // Japanese
                               5, // Russian,
                               87, // Danish
                               90, // Dutch
                               94 // Dutch
                           }.AsQueryable();
            }
        }
    }
}
