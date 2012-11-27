using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class BooksController : EntityController<Book>
    {
        public BooksController(IEntityRepository<Book> repository)
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
