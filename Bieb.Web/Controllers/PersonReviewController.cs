using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class PersonReviewController : EntityController<Review<Person>>
    {
        public PersonReviewController(IEntityRepository<Review<Person>> repository)
            : base(repository)
        { }
    }
}
