using System;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Controllers
{
    public class StoriesController : EntityController<Story, EditStoryModel>
    {
        public StoriesController(IEntityRepository<Story> repository, EditEntityModelMapper<Story, EditStoryModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }

        public override ActionResult Index(int pageSize = 25, int page = 1)
        {
            throw new NotImplementedException("Listing an index of stories is not allowed. You can get there via Books and Persons");
        }

        protected override System.Linq.Expressions.Expression<Func<Story, IComparable>> SortFunc
        {
            get { return s => s.TitleSort; }
        }
    }
}
