using System;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class PublisherController : EntityController<Publisher>
    {
        public PublisherController(IEntityRepository<Publisher> repository)
            : base(repository)
        { }

        public override ActionResult Index(int pageSize = 25, int page = 1)
        {
            throw new NotImplementedException("Listing an index of publishers is not allowed. You can get there via Books and Stories");
        }

        protected override System.Linq.Expressions.Expression<Func<Publisher, IComparable>> SortFunc
        {
            get { return p => p.Name; }
        }
    }
}
