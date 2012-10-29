using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class PersonController : EntityController<Person>
    {
        public PersonController(IEntityRepository<Person> repository)
            : base(repository)
        { }


        protected override System.Linq.Expressions.Expression<Func<Person, IComparable>> SortFunc
        {
            get { return p => p.Surname; }
        }
    }
}
