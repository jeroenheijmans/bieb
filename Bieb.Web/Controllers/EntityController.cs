using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using PagedList;

namespace Bieb.Web.Controllers
{
    public abstract class EntityController<T> : Controller where T : BaseEntity, new()
    {
        protected int[] availablePageSizes = new int[] { 10, 25, 50, 100 };
        protected int defaultPageSize = 25;
        protected IEntityRepository<T> Repository { get; set; }
        
        protected EntityController(IEntityRepository<T> repository)
        {
            this.Repository = repository;
        }
        
        public ActionResult Index(int pageSize = 25, int page = 1)
        {
            var items = Repository
                        .Items
                        .ToPagedList(page, pageSize);

            return View(items);

        }

        public ActionResult Details(int id)
        {
            return View(Repository.GetItem(id));
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

    }
}
