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
        where TViewModel : BaseDomainObjectCrudModel<TEntity>, new()
    {
        protected int[] AvailablePageSizes = new[] { 10, 25, 50, 100 };
        protected int DefaultPageSize = 25;
        protected IEntityRepository<TEntity> Repository { get; set; }


        protected EntityController(IEntityRepository<TEntity> repository)
            : base(null)
        {
            this.Repository = repository;
        }

        protected EntityController(IEntityRepository<TEntity> repository, HttpResponseBase customResponse)
            : base(customResponse)
        {
            this.Repository = repository;
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
            var model = GetEditViewModel(new TEntity());
            return View("Edit", model);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TViewModel model)
        {
            var item = model.MergeWithEntity(new TEntity());
            Repository.Add(item);
            return RedirectToAction("Details", new { id = item.Id });
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var item = Repository.GetItem(id);

            if (item == null)
            {
                return PageNotFound();
            }

            var model = GetEditViewModel(item);
            return View(model);
        }

        private static object GetEditViewModel(TEntity entity)
        {
            return Activator.CreateInstance(typeof(TViewModel), new object[] { entity });
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

        private ActionResult SaveExistingEntity(TViewModel model, TEntity existingEntity)
        {
            var entity = model.MergeWithEntity(existingEntity);
            Repository.NotifyItemWasChanged(entity);
            return RedirectToAction("Details", new {id = entity.Id});
        }
    }
}
