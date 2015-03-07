using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Localization;

namespace Bieb.Web.Models
{
    public class EditPublishableModelMapper<TEntity, TModel> : EditEntityModelMapper<TEntity, TModel>
        where TEntity : Publishable
        where TModel : EditPublishableModel<TEntity>, new()
    {
        private readonly IEnumerable<Publisher> publishers;
        private readonly IBookRepository books;
        private readonly IIsbnLanguageDisplayer isbnLanguageDisplayer;
        protected readonly IEnumerable<Person> people;

        public EditPublishableModelMapper(IEntityRepository<Publisher> publishers, IEntityRepository<Person> people, IBookRepository books, IIsbnLanguageDisplayer isbnLanguageDisplayer)
        {
            this.publishers = publishers.Items;
            this.people = people.Items;
            this.books = books;
            this.isbnLanguageDisplayer = isbnLanguageDisplayer;
        }

        public override void MergeEntityWithModel(TEntity entity, TModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.IsbnLanguage = model.IsbnLanguage;
            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
            entity.Year = model.Year;
            entity.Publisher = publishers.FirstOrDefault(p => p.Id == model.PublisherId);


            entity.ClearAuthors();

            foreach (var authorId in model.AuthorIds)
            {
                var person = people.FirstOrDefault(p => p.Id == authorId);

                if (person == null)
                {
                    throw new MappingException("Provided Author Id could not be traced to any person in the database.");
                }

                entity.AddAuthor(person);
            }


            entity.ClearTranslators();

            foreach (var translatorId in model.TranslatorIds)
            {
                var person = people.FirstOrDefault(p => p.Id == translatorId);

                if (person == null)
                {
                    throw new MappingException("Provided Translator Id could not be traced to any person in the database.");
                }

                entity.AddTranslator(person);
            }
        }

        public override TModel ModelFromEntity(TEntity entity)
        {
            var model = base.ModelFromEntity(entity);

            model.AvailablePublishers = new SelectList(publishers.OrderBy(p => p.Name), "Id", "Name");
            model.AvailablePeople = new SelectList(people.OrderBy(p => p.Surname).ThenBy(p => p.FirstName), "Id", "FullNameAlphabetical");

            model.AvailableIsbnLanguages = new SelectList(GetLanguageOptions(), "Key", "Value");

            model.IsbnLanguage = entity.IsbnLanguage;
            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;
            model.Year = entity.Year;

            model.PublisherId = entity.Publisher == null ? (int?)null : entity.Publisher.Id;

            model.AuthorIds = entity.Authors.Select(a => a.Id).ToArray();
            model.TranslatorIds = entity.Translators.Select(t => t.Id).ToArray();
            
            return model;
        }


        protected IEnumerable<KeyValuePair<string, string>> GetLanguageOptions()
        {
            return books.IsbnLanguages.ToDictionary(id => id.ToString(), id => isbnLanguageDisplayer.GetLocalizedIsbnLanguageResourceForAdmins(id));
        }
    }
}