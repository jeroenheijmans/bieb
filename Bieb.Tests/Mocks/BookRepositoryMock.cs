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
        public IQueryable<int> IsbnLanguages
        {
            get { return new [] {1, 2, 3}.AsQueryable(); }
        }
    }
}
