using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class SeriesController : EntityController<Series>
    {
        public SeriesController(IEntityRepository<Series> repository)
            : base(repository)
        { }



        protected override System.Linq.Expressions.Expression<Func<Series, IComparable>> SortFunc
        {
            get { return s => s.Title; }
        }
    }
}
