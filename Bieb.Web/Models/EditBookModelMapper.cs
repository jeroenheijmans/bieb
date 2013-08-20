using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditBookModelMapper : EditEntityModelMapper<Book, EditBookModel>
    {
        public override void MergeEntityWithModel(Book entity, EditBookModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Isbn = model.Isbn;
            entity.IsbnLanguage = model.IsbnLanguage;
            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
            entity.Year = model.Year;
            entity.LibraryStatus = model.LibraryStatus;
            // entity.Publisher = Publisher; // TODO
        }

        public override EditBookModel ModelFromEntity(Book entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Isbn = entity.Isbn;
            model.IsbnLanguage = entity.IsbnLanguage;
            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;
            model.Year = entity.Year;
            model.LibraryStatus = entity.LibraryStatus;
            model.Publisher = entity.Publisher;

            return model;
        }
    }
}