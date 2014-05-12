using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.People;

[assembly: InternalsVisibleTo("Bieb.Tests")]

namespace Bieb.Web.Controllers
{
    public class PeopleController : EntityController<Person, EditPersonModel>
    {
        private const int MaxNumberOfBirthDates = 5;

        public PeopleController(IEntityRepository<Person> repository, EditEntityModelMapper<Person, EditPersonModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }
        
        protected override System.Linq.Expressions.Expression<Func<Person, IComparable>> SortFunc
        {
            get { return p => p.Surname; }
        }

        public ActionResult TodaysBirthDates()
        {
            return BirthDates(DateTime.Now);
        }

        internal ActionResult BirthDates(DateTime referenceDate)
        {
            var people = Repository.Items
                            .Where(p => p.DateOfBirthFrom.HasValue
                                        && p.DateOfBirthUntil.HasValue
                                        && p.DateOfBirthFrom.Value.Equals(p.DateOfBirthUntil.Value) // would prefer p.DateOfBirth.IsCertain, but that won't roll with NHibernate
                                        && p.DateOfBirthFrom.Value.Day == referenceDate.Day
                                        && p.DateOfBirthFrom.Value.Month == referenceDate.Month)
                            .Take(MaxNumberOfBirthDates);

            return PartialView(people);
        }
    }
}