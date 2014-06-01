using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;

namespace Bieb.Web.Controllers
{
    public class BooksController : EntityController<Book, ViewBookModel, EditBookModel>
    {
        public BooksController(IEntityRepository<Book> repository,
                               IViewEntityModelMapper<Book, ViewBookModel> viewEntityModelMapper,
                               IEditEntityModelMapper<Book, EditBookModel> editEntityModelMapper)
            : base(repository, viewEntityModelMapper, editEntityModelMapper)
        { }

        public BooksController(IEntityRepository<Book> repository,
                               IViewEntityModelMapper<Book, ViewBookModel> viewEntityModelMapper,
                               IEditEntityModelMapper<Book, EditBookModel> editEntityModelMapper, 
                               HttpResponseBase customResponse)
            : base(repository, viewEntityModelMapper, editEntityModelMapper, customResponse)
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
                return book => book.LibraryStatus != LibraryStatus.OnlyForReference;
            }
        }
    }
}
