using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Localization;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Books
{
    public class EditBookModelMapper : EditEntityModelMapper<Book, EditBookModel>
    {
        private readonly IEnumerable<Publisher> publishers;
        private readonly IEnumerable<Person> people;
        private readonly IBookRepository bookRepository;
        private readonly EditStoryModelMapper storyMapper;
        private readonly IIsbnLanguageDisplayer isbnLanguageDisplayer;

        public EditBookModelMapper(IEntityRepository<Publisher> publishers, IEntityRepository<Person> people, IBookRepository books, EditStoryModelMapper storyMapper, IIsbnLanguageDisplayer isbnLanguageDisplayer)
        {
            this.publishers = publishers.Items;
            this.people = people.Items;
            this.bookRepository = books;
            this.storyMapper = storyMapper;
            this.isbnLanguageDisplayer = isbnLanguageDisplayer;
        }

        public override void MergeEntityWithModel(Book entity, EditBookModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Isbn = model.Isbn;
            entity.IsbnLanguage = model.IsbnLanguage;
            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
            entity.Year = model.Year;
            entity.LibraryStatus = model.LibraryStatus;
            entity.ReviewText = model.ReviewText;

            entity.Publisher = publishers.FirstOrDefault(p => p.Id == model.PublisherId);


            // TODO: Refactor, the code below is copy-pasted from this
            entity.Editors.Clear();

            foreach (var editorId in model.EditorIds)
            {
                var person = people.FirstOrDefault(p => p.Id == editorId);

                if (person == null)
                {
                    throw new MappingException("Provided Editor Id could not be traced to any person in the database.");
                }

                entity.Editors.Add(person);
            }

            // TODO: Refactor, the code below is copy-paste from the above
            entity.Authors.Clear();

            foreach (var authorId in model.AuthorIds)
            {
                var person = people.FirstOrDefault(p => p.Id == authorId);

                if (person == null)
                {
                    throw new MappingException("Provided Author Id could not be traced to any person in the database.");
                }

                entity.Authors.Add(person);
            }

            // TODO: Refactor, the code below is copy-paste from the above
            entity.Translators.Clear();

            foreach (var translatorId in model.TranslatorIds)
            {
                var person = people.FirstOrDefault(p => p.Id == translatorId);

                if (person == null)
                {
                    throw new MappingException("Provided Translator Id could not be traced to any person in the database.");
                }

                entity.Translators.Add(person);
            }


            foreach (var storyModel in model.Stories)
            {
                var storyEntity = entity.Stories.FirstOrDefault(s => s.Value.Id == storyModel.Id).Value;

                if (storyEntity == null)
                {
                    storyEntity = new Story();
                    entity.AddStory(storyEntity);
                }

                storyMapper.MergeEntityWithModel(storyEntity, storyModel);
            }

            foreach (var storyModel in model.NewStories.Where(s => !string.IsNullOrEmpty(s.Title)))
            {
                var storyEntity = new Story();
                entity.AddStory(storyEntity);
                storyMapper.MergeEntityWithModel(storyEntity, storyModel);
            }
        }

        public override EditBookModel ModelFromEntity(Book entity)
        {
            var model = base.ModelFromEntity(entity);

            model.AvailablePublishers = new SelectList(publishers.OrderBy(p => p.Name), "Id", "Name");
            model.AvailablePeople = new SelectList(people.OrderBy(p => p.Surname).ThenBy(p => p.FirstName), "Id", "FullNameAlphabetical");

            model.AvailableIsbnLanguages =
                new SelectList(bookRepository.IsbnLanguages.Select(l => new {Value = l, Text = isbnLanguageDisplayer.GetLocalizedIsbnLanguageResourceForAdmins(l)}),
                               "Value", "Text");

            model.Isbn = entity.Isbn;
            model.IsbnLanguage = entity.IsbnLanguage;
            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;
            model.Year = entity.Year;
            model.LibraryStatus = entity.LibraryStatus;
            model.ReviewText = entity.ReviewText;

            model.PublisherId = entity.Publisher == null ? (int?)null : entity.Publisher.Id;

            model.EditorIds = entity.Editors.Select(e => e.Id).ToArray();
            model.AuthorIds = entity.Authors.Select(a => a.Id).ToArray();
            model.TranslatorIds = entity.Translators.Select(t => t.Id).ToArray();

            model.Stories = entity.Stories.Select(s => storyMapper.ModelFromEntity(s.Value)).ToList();

            return model;
        }
    }
}