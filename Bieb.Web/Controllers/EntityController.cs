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
    public abstract class EntityController<TEntity, TViewModel> : BaseController 
        where TEntity : BaseEntity, new()
        where TViewModel : EditEntityModel<TEntity>, new()
    {
        protected int[] AvailablePageSizes = new[] { 10, 25, 50, 100 };
        protected int DefaultPageSize = 25;
        protected IEntityRepository<TEntity> Repository { get; set; }
        protected EditEntityModelMapper<TEntity, TViewModel> EditEntityModelMapper { get; set; }


        protected EntityController(IEntityRepository<TEntity> repository, EditEntityModelMapper<TEntity, TViewModel> editEntityModelMapper)
            : base(null)
        {
            this.Repository = repository;
            this.EditEntityModelMapper = editEntityModelMapper;
        }

        protected EntityController(IEntityRepository<TEntity> repository, EditEntityModelMapper<TEntity, TViewModel> editEntityModelMapper, HttpResponseBase customResponse)
            : base(customResponse)
        {
            this.Repository = repository;
            this.EditEntityModelMapper = editEntityModelMapper;
        }
        

        public virtual ActionResult Index(int pageSize = 25, int page = 1)
        {
            var items = Repository
                        .Items
                        .Where(IndexFilterFunc)
                        .OrderBy(SortFunc)
                        .ToPagedList(page, pageSize);
            
            return View(items);
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

            return View(item);
        }


        public ActionResult RecentlyAdded()
        {
            var item = Repository
                .Items
                .OrderByDescending(i => i.CreatedDate)
                .ThenByDescending(i => i.Id)
                .FirstOrDefault();

            return PartialView(item);
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
        public ActionResult Create(TViewModel model)
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
        public ActionResult Save(TViewModel model)
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

        private ActionResult SaveExistingEntity(TViewModel model, TEntity existingEntity)
        {
            EditEntityModelMapper.MergeEntityWithModel(existingEntity, model);
            Repository.NotifyItemWasChanged(existingEntity);
            return RedirectToAction("Details", new { id = existingEntity.Id });
        }
    }
}
