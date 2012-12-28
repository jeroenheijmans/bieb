using System;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class LibraryBooksController : EntityController<LibraryBook>
    {
        public LibraryBooksController(IEntityRepository<LibraryBook> repository)
            : base(repository)
        { }

        protected override System.Linq.Expressions.Expression<Func<LibraryBook, IComparable>> SortFunc
        {
            get
            {
                return b => b.TitleSort;
            }
        }
    }
}
