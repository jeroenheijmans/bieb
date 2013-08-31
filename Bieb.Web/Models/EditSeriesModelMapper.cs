using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Models
{
    public class EditSeriesModelMapper : EditEntityModelMapper<Series, EditSeriesModel>
    {
        private IEnumerable<Book> books; 

        public EditSeriesModelMapper(IEntityRepository<Book> books)
        {
            this.books = books.Items;
        }

        public override void MergeEntityWithModel(Series entity, EditSeriesModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
        }

        public override EditSeriesModel ModelFromEntity(Series entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;

            model.AvailableBooks = new SelectList(books.OrderBy(b => b.Title), "Id", "TitleSort");

            return model;
        }
    }
}