using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class PersonReviewsController : EntityController<Review<Person>>
    {
        public PersonReviewsController(IEntityRepository<Review<Person>> repository)
            : base(repository)
        { }

        protected override System.Linq.Expressions.Expression<Func<Review<Person>, IComparable>> SortFunc
        {
            get { return pr => pr.Rating; }
        }
    }
}
