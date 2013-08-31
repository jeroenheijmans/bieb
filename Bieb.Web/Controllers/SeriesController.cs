using System;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;

namespace Bieb.Web.Controllers
{
    public class SeriesController : EntityController<Series, EditSeriesModel>
    {
        public SeriesController(IEntityRepository<Series> repository, EditEntityModelMapper<Series, EditSeriesModel> editEntityModelMapper)
            : base(repository, editEntityModelMapper)
        { }
        

        protected override System.Linq.Expressions.Expression<Func<Series, IComparable>> SortFunc
        {
            get { return s => s.TitleSort; }
        }
    }
}
