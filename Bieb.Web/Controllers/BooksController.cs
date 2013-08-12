using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class BooksController : EntityController<LibraryBook, LibraryBookModel>
    {
        public BooksController(IEntityRepository<LibraryBook> repository)
            : base(repository)
        { }

        public BooksController(IEntityRepository<LibraryBook> repository, HttpResponseBase customResponse)
            : base(repository, customResponse)
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
