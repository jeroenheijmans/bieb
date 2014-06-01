using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.People;

namespace Bieb.Web.Controllers
{
    public class PersonReviewsController : EntityController<Review<Person>, EditPersonReviewModel>
    {
        public PersonReviewsController(IEntityRepository<Review<Person>> repository, EditEntityModelMapper<Review<Person>, EditPersonReviewModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }

        protected override System.Linq.Expressions.Expression<Func<Review<Person>, IComparable>> SortFunc
        {
            get { return pr => pr.Rating; }
        }
    }
}
