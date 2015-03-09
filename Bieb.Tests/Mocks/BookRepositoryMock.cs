using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Tests.Mocks
{
    public class BookRepositoryMock : RepositoryMock<Book>, IBookRepository
    {
        private IList<string> languages = new List<string>(new[] { "nl", "en", "ru" });

        public void SetNewIso639LanguageIds(IEnumerable<string> newIds)
        {
            languages = new List<string>(newIds);
        }

        public IQueryable<string> Iso639LanguageIdentifiers
        {
            get { return languages.AsQueryable(); }
        }
    }
}
