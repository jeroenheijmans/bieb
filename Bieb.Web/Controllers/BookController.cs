using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using PagedList;

namespace Bieb.Web.Controllers
{
    public class BookController : EntityController<Book>
    {
        public BookController(IEntityRepository<Book> repository)
            : base(repository)
        { }

        protected override System.Linq.Expressions.Expression<Func<Book, IComparable>> SortFunc
        {
            get
            {
                return b => b.TitleSort;
            }
        }
    }
}
