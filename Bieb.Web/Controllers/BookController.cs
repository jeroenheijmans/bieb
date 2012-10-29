using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

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
