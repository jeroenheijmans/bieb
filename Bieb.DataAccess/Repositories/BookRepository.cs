using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Bieb.DataAccess.Repositories
{
    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public BookRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        { }

        protected override ICriteria AmendGetRandomItemCriterion(ICriteria input)
        {
            return input.Add(Restrictions.Where<Book>(b => b.LibraryStatus != LibraryStatus.OnlyForReference));
        }

        public IQueryable<string> Iso639LanguageIdentifiers
        {
            get
            {
                // TODO: This should come from another source
                return new[]
                           {
                               "en",
                               "nl"
                           }.AsQueryable();
            }
        }
    }
}
