using System;
using System.Web.Mvc;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class ReferenceBooksController : EntityController<ReferenceBook, ReferenceBookModel>
    {
        public ReferenceBooksController(IEntityRepository<ReferenceBook> repository)
            : base(repository)
        { }

        protected override System.Linq.Expressions.Expression<Func<ReferenceBook, IComparable>> SortFunc
        {
            get
            {
                return b => b.TitleSort;
            }
        }
    }
}
