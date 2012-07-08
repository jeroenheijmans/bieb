using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class BookController : EntityController<Book>
    {
        public BookController(IEntityRepository<Book> Repository) : base(Repository)
        { }
    }
}
