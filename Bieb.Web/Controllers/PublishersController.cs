using System;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class PublishersController : EntityController<Publisher, EditPublisherModel>
    {
        public PublishersController(IEntityRepository<Publisher> repository, EditEntityModelMapper<Publisher, EditPublisherModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }
        
        protected override System.Linq.Expressions.Expression<Func<Publisher, IComparable>> SortFunc
        {
            get { return p => p.Name; }
        }
    }
}
