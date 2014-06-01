using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using PagedList;

namespace Bieb.Web.Controllers
{
    public abstract class EntityController<TEntity, TViewModel, TEditModel> : BaseController 
        where TEntity : BaseEntity, new()
        where TViewModel : ViewEntityModel<TEntity>
        where TEditModel : EditEntityModel<TEntity>, new()
    {
        protected int[] AvailablePageSizes = new[] { 10, 25, 50, 100 };
        protected int DefaultPageSize = 25;
        protected IEntityRepository<TEntity> Repository { get; set; }
        protected IEditEntityModelMapper<TEntity, TEditModel> EditEntityModelMapper { get; set; }
        protected IViewEntityModelMapper<TEntity, TViewModel> ViewEntityModelMapper { get; set; }


        protected EntityController(IEntityRepository<TEntity> repository,
                                   IViewEntityModelMapper<TEntity, TViewModel> viewEntityModelMapper,
                                   IEditEntityModelMapper<TEntity, TEditModel> editEntityModelMapper)
            : this(repository, viewEntityModelMapper, editEntityModelMapper, null)
        {}

        protected EntityController(IEntityRepository<TEntity> repository,
                                   IViewEntityModelMapper<TEntity, TViewModel> viewEntityModelMapper,
                                   IEditEntityModelMapper<TEntity, TEditModel> editEntityModelMapper, 
                                   HttpResponseBase customResponse)
            : base(customResponse)
        {
            if (viewEntityModelMapper == null)
            {
                throw new ArgumentNullException("viewEntityModelMapper");
            }

            if (editEntityModelMapper == null)
            {
                throw new ArgumentNullException("editEntityModelMapper");
            }

            this.Repository = repository;
            this.EditEntityModelMapper = editEntityModelMapper;
            this.ViewEntityModelMapper = viewEntityModelMapper;
        }
        

        public virtual ActionResult Index(int pageSize = 25, int page = 1)
        {
            var items = Repository
                        .Items
                        .Where(IndexFilterFunc)
                        .OrderBy(SortFunc);

            var totalItemCount = items.Count();

            var pageIndex = page - 1; // Linq "Skip" expects first page to be zero
            
            var viewModels = items
                             .Skip(pageIndex * pageSize)
                             .Take(pageSize)
                             .Select(i => ViewEntityModelMapper.ModelFromEntity(i));

            var pagedList = new StaticPagedList<TViewModel>(viewModels, page, pageSize, totalItemCount);
            
            return View(pagedList);
        }


        protected abstract Expression<Func<TEntity, IComparable>> SortFunc { get; }

        protected virtual Expression<Func<TEntity, bool>> IndexFilterFunc 
        { 
            get { return entity => true; }
        } 


        public ActionResult Details(int id)
        {
            var item = Repository.GetItem(id);

            if (item == null)
            {
                return PageNotFound();
            }

            var model = ViewEntityModelMapper.ModelFromEntity(item);

            return View(model);
        }


        public ActionResult RecentlyAdded()
        {
            var item = Repository
                .Items
                .OrderByDescending(i => i.CreatedDate)
                .ThenByDescending(i => i.Id)
                .FirstOrDefault();

            if (item == null)
            {
                return PartialView(null);
            }

            var model = ViewEntityModelMapper.ModelFromEntity(item);

            return PartialView(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = EditEntityModelMapper.ModelFromEntity(new TEntity());
            return View("Edit", model);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TEditModel model)
        {
            var entity = new TEntity();

            EditEntityModelMapper.MergeEntityWithModel(entity, model);

            Repository.Add(entity);

            return RedirectToAction("Details", new { id = entity.Id });
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var item = Repository.GetItem(id);

            if (item == null)
            {
                return PageNotFound();
            }

            var model = EditEntityModelMapper.ModelFromEntity(item);

            return View(model);
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            
            var existingEntity = Repository.GetItem(model.Id);

            if (existingEntity == null)
            {
                return Create(model);
            }
         
            return SaveExistingEntity(model, existingEntity);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var item = Repository.GetItem(id);

            if (item == null)
            {
                return PageNotFound();
            }

            Repository.Remove(item);
            return RedirectToAction("Index");
        }

        private ActionResult SaveExistingEntity(TEditModel model, TEntity existingEntity)
        {
            EditEntityModelMapper.MergeEntityWithModel(existingEntity, model);
            Repository.NotifyItemWasChanged(existingEntity);
            return RedirectToAction("Details", new { id = existingEntity.Id });
        }
    }
}
