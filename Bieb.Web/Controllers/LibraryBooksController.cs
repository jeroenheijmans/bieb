using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class LibraryBooksController : EntityController<LibraryBook, LibraryBookModel>
    {
        public LibraryBooksController(IEntityRepository<LibraryBook> repository)
            : base(repository)
        { }

        public LibraryBooksController(IEntityRepository<LibraryBook> repository, HttpResponseBase customResponse, HttpContextBase customContext)
            : base(repository, customResponse, customContext)
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
