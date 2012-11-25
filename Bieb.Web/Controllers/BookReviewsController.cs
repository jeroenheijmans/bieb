using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class BookReviewsController : EntityController<Review<Book>> 
    {
        public BookReviewsController(IEntityRepository<Review<Book>> repository)
            : base(repository)
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
