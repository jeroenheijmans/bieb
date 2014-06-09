using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Controllers
{
    public class StoriesController : EntityController<Story, ViewStoryModel, EditStoryModel>
    {
        public StoriesController(IEntityRepository<Story> repository,
                                 IViewEntityModelMapper<Story, ViewStoryModel> viewStoryModelMapper,
                                 IEditEntityModelMapper<Story, EditStoryModel> editEntityModelMapper)
            : base(repository, viewStoryModelMapper, editEntityModelMapper)
        { }

        public override ActionResult Index(int pageSize = 25, int page = 1)
        {
            return RedirectToAction("Index", "Books");
        }

        [ExcludeFromCodeCoverage] // TODO: this property is overridden because we *have* to, but it's of no use currently.
        protected override System.Linq.Expressions.Expression<Func<Story, IComparable>> SortFunc
        {
            get { return s => s.TitleSort; }
        }
    }
}
