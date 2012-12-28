using System;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using PagedList;

namespace Bieb.Web.Controllers
{
    public abstract class EntityController<T> : Controller where T : BaseEntity, new()
    {
        protected int[] availablePageSizes = new[] { 10, 25, 50, 100 };
        protected int defaultPageSize = 25;
        protected IEntityRepository<T> Repository { get; set; }
        
        protected EntityController(IEntityRepository<T> repository)
        {
            this.Repository = repository;
        }
        
        public virtual ActionResult Index(int pageSize = 25, int page = 1)
        {
            var items = Repository
                        .Items
                        .OrderBy(SortFunc)
                        .ToPagedList(page, pageSize);
            
            return View(items);
        }

        protected abstract System.Linq.Expressions.Expression<Func<T, IComparable>> SortFunc { get; }

        public ActionResult Details(int id)
        {
            return View(Repository.GetItem(id));
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
        public ActionResult Create()
        {
            return View(new T());
        }

        [HttpPost]
        public ActionResult Create(T item)
        {
            Repository.Save(item);
            return RedirectToAction("Details", new { id = item.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Repository.GetItem(id));
        }

        [HttpPost]
        public ActionResult Save(T item)
        {
            Repository.Save(item);
            return RedirectToAction("Details", new { id = item.Id });
        }

    }
}
