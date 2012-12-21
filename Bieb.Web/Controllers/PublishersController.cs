using System;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class PublishersController : EntityController<Publisher>
    {
        public PublishersController(IEntityRepository<Publisher> repository)
            : base(repository)
        { }
        
        protected override System.Linq.Expressions.Expression<Func<Publisher, IComparable>> SortFunc
        {
            get { return p => p.Name; }
        }
    }
}
