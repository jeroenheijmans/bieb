using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Localization;
using Bieb.Web.Models.Books;

namespace Bieb.Web.Models.Stories
{
    public class EditStoryModelMapper : EditPublishableModelMapper<Story, EditStoryModel>
    {
        public EditStoryModelMapper(IEntityRepository<Publisher> publishers, IEntityRepository<Person> people, IBookRepository books, IIso639LanguageDisplayer iso639LanguageDisplayer)
            : base(publishers, people, books, iso639LanguageDisplayer)
        {}

        public override void MergeEntityWithModel(Story entity, EditStoryModel model)
        {
            base.MergeEntityWithModel(entity, model);
        }

        public override EditStoryModel ModelFromEntity(Story entity)
        {
            var model = base.ModelFromEntity(entity);

            model.BookTitle = entity.Book.Title;

            return model;
        }
    }
}