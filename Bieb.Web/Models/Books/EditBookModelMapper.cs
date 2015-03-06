﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Localization;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Books
{
    public class EditBookModelMapper : EditPublishableModelMapper<Book, EditBookModel>
    {
        private readonly EditStoryModelMapper storyMapper;
        

        public EditBookModelMapper(IEntityRepository<Publisher> publishers, IEntityRepository<Person> people, IBookRepository books, EditStoryModelMapper storyMapper, IIsbnLanguageDisplayer isbnLanguageDisplayer)
            : base(publishers, people, books, isbnLanguageDisplayer)
        {
            this.storyMapper = storyMapper;
        }

        public override void MergeEntityWithModel(Book entity, EditBookModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Isbn = model.Isbn;
            entity.LibraryStatus = model.LibraryStatus;
            entity.ReviewText = model.ReviewText;


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
            
            model.AvailableLibraryStatuses = new SelectList(GetLibraryStatusOptions(), "Key", "Value");

            model.Isbn = entity.Isbn;
            model.LibraryStatus = entity.LibraryStatus;
            model.ReviewText = entity.ReviewText;

            model.PublisherId = entity.Publisher == null ? (int?)null : entity.Publisher.Id;

            model.EditorIds = entity.Editors.Select(e => e.Id).ToArray();

            model.Stories = entity.Stories.Select(s => storyMapper.ModelFromEntity(s.Value)).ToList();

            return model;
        }


        private static IEnumerable<KeyValuePair<LibraryStatus, string>> GetLibraryStatusOptions()
        {
            return Enum.GetValues(typeof(LibraryStatus)).Cast<LibraryStatus>().ToDictionary(s => s, EnumDisplayer.GetResource);
        } 
    }
}