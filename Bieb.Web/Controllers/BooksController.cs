using System;
using System.Web.Mvc;
using System.Linq;
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

        public ActionResult RecentlyAdded()
        {
            var book = Repository.Items.OrderByDescending(b => b.Id).First();
            return PartialView(book);
        }
    }
}
