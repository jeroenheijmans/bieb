using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class BooksController : EntityController<Book, BookModel>
    {
        public BooksController(IEntityRepository<Book> repository)
            : base(repository)
        { }

        public BooksController(IEntityRepository<Book> repository, HttpResponseBase customResponse)
            : base(repository, customResponse)
        { }

        protected override System.Linq.Expressions.Expression<Func<Book, IComparable>> SortFunc
        {
            get
            {
                return b => b.TitleSort;
            }
        }

        protected override System.Linq.Expressions.Expression<Func<Book, bool>> IndexFilterFunc
        {
            get
            {
                return book => book is LibraryBook;
            }
        }
    }
}
