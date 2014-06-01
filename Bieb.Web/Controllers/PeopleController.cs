using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.People;

namespace Bieb.Web.Controllers
{
    public class PeopleController : EntityController<Person, ViewPersonModel, EditPersonModel>
    {
        private const int MaxNumberOfBirthDates = 5;

        public PeopleController(IEntityRepository<Person> repository,
                                IViewEntityModelMapper<Person, ViewPersonModel> viewEntityModelMapper,
                                IEditEntityModelMapper<Person, EditPersonModel> editEntityModelMapper)
            : base(repository, viewEntityModelMapper, editEntityModelMapper)
        { }
        
        protected override System.Linq.Expressions.Expression<Func<Person, IComparable>> SortFunc
        {
            get { return p => p.Surname; }
        }

        public ActionResult TodaysBirthDates()
        {
            return BirthDates(DateTime.Now);
        }

        public ActionResult BirthDates(DateTime referenceDate)
        {
            var people = Repository.Items
                            .Where(p => p.DateOfBirthFrom.HasValue
                                        && p.DateOfBirthUntil.HasValue
                                        && p.DateOfBirthFrom.Value.Equals(p.DateOfBirthUntil.Value) // would prefer p.DateOfBirth.IsCertain, but that won't roll with NHibernate
                                        && p.DateOfBirthFrom.Value.Day == referenceDate.Day
                                        && p.DateOfBirthFrom.Value.Month == referenceDate.Month)
                            .Take(MaxNumberOfBirthDates)
                            .Select(p => p.AsLinkablePersonModel());

            return PartialView(people);
        }
    }
}