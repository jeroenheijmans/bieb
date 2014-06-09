using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;

namespace Bieb.Web.Controllers
{
    public class BookReviewsController : EntityController<Review<Book>, ViewBookReviewModel, EditBookReviewModel> 
    {
        public BookReviewsController(IEntityRepository<Review<Book>> repository, 
                                     IViewEntityModelMapper<Review<Book>, ViewBookReviewModel> viewEntityModelMapper,
                                     IEditEntityModelMapper<Review<Book>, EditBookReviewModel> editEntityModelMapper)
            : base(repository, viewEntityModelMapper, editEntityModelMapper)
        { }

        protected override System.Linq.Expressions.Expression<Func<Review<Book>, IComparable>> SortFunc
        {
            get 
            {
                return br => -br.Rating;
            }
        }
    }
}
