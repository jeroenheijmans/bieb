using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class StoryController : EntityController<Story>
    {
        public StoryController(IEntityRepository<Story> repository)
            : base(repository)
        { }

        public override ActionResult Index(int pageSize = 25, int page = 1)
        {
            throw new NotImplementedException("Listing an index of stories is not allowed. You can get there via Books and Persons");
        }
    }
}
