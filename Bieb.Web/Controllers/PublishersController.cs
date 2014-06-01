using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Publishers;

namespace Bieb.Web.Controllers
{
    public class PublishersController : EntityController<Publisher, ViewPublisherModel, EditPublisherModel>
    {
        public PublishersController(IEntityRepository<Publisher> repository,
                                    IViewEntityModelMapper<Publisher, ViewPublisherModel> viewEntityModelMapper,
                                    IEditEntityModelMapper<Publisher, EditPublisherModel> editEntityModelMapper)
            : base(repository, viewEntityModelMapper, editEntityModelMapper)
        { }
        
        protected override System.Linq.Expressions.Expression<Func<Publisher, IComparable>> SortFunc
        {
            get { return p => p.Name; }
        }
    }
}
