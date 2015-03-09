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

        public IEnumerable<string> Iso639LanguageIdentifiers
        {
            get
            {
                return new[]
                           {
                               "af", "ar", "bn", "bs", "bg", "my", "ca", "zh", "hr", "cs", "da", "nl", "en", "et", "fi", "fr", "ka", "de", "el", "he", "hi",
                               "hu", "is", "id", "ga", "it", "ja", "jv", "ko", "ku", "lv", "lt", "mk", "ms", "mr", "no", "pa", "fa", "pl", "pt", "ro", "ru",
                               "sa", "gd", "sr", "sk", "sl", "es", "sv", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi"
                           };
            }
        }
    }
}
