using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class BookReviewsController : EntityController<Review<Book>, EditBookReviewModel> 
    {
        public BookReviewsController(IEntityRepository<Review<Book>> repository, EditEntityModelMapper<Review<Book>, EditBookReviewModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }

        protected override System.Linq.Expressions.Expression<Func<Review<Book>, IComparable>> SortFunc
        {
            get 
            {
                return br => br.Rating;            
            }
        }
    }
}
