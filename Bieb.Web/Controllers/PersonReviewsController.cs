using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.People;

namespace Bieb.Web.Controllers
{
    public class PersonReviewsController : EntityController<Review<Person>, ViewPersonReviewModel, EditPersonReviewModel>
    {
        public PersonReviewsController(IEntityRepository<Review<Person>> repository,
                                       IViewEntityModelMapper<Review<Person>, ViewPersonReviewModel> viewEntityModelMapper,
                                       IEditEntityModelMapper<Review<Person>, EditPersonReviewModel> editEntityModelMapper)
            : base(repository, viewEntityModelMapper, editEntityModelMapper)
        { }

        protected override System.Linq.Expressions.Expression<Func<Review<Person>, IComparable>> SortFunc
        {
            get { return pr => -pr.Rating; }
        }
    }
}
