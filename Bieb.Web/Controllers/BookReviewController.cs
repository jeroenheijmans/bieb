using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class BookReviewController : EntityController<Review<Book>> 
    {
        public BookReviewController(IEntityRepository<Review<Book>> repository)
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
