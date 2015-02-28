using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Tests.Mocks
{
    public class BookRepositoryMock : RepositoryMock<Book>, IBookRepository
    {
        private IList<int> languages = new List<int>(new[] { 1, 2, 3 });  

        public void SetNewIsbnLanguageIds(IEnumerable<int> newIds)
        {
            languages = new List<int>(newIds);
        }

        public IQueryable<int> IsbnLanguages
        {
            get { return languages.AsQueryable(); }
        }
    }
}
