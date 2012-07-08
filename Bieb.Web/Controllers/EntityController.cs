using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public abstract class EntityController<T> : Controller where T : BaseEntity
    {
        protected IEntityRepository<T> Repository { get; set; }
        
        public EntityController(IEntityRepository<T> repository)
        {
            this.Repository = repository;
        }

        public ActionResult Index()
        {            
            return View(Repository.Items);
        }

        public ActionResult Details(int id)
        {
            return View(Repository.Get(id));
        }

    }
}
