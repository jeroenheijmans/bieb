﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditBookModelMapperTests
    {
        private Publisher somePublisher;
        private IEntityRepository<Publisher> publishers;
        private IEntityRepository<Person> people;
        private IBookRepository books;
        private EditStoryModelMapper storyMapper;
        private EditBookModelMapper mapper;
        private Person asimov, adams, wyndham;
        private Story story1, story2, story3;

        
        [SetUp]
        public void SetUp()
        {
            somePublisher = new Publisher { Id = 42, Name = "Penguin Books" };
            publishers = new RepositoryMock<Publisher>();
            people = new RepositoryMock<Person>();
            books = new BookRepositoryMock();

            var iso639LanguageDisplayer = new Mock<IIso639LanguageDisplayer>();

            storyMapper = new EditStoryModelMapper(publishers, people, books, iso639LanguageDisplayer.Object);
            mapper = new EditBookModelMapper(publishers, people, books, storyMapper, iso639LanguageDisplayer.Object);

            asimov = new Person {Id = 1, FirstName = "Isaac", Surname = "Asimov"};
            adams = new Person {Id = 2, FirstName = "Douglas", Surname = "Adams"};
            wyndham = new Person {Id = 3, FirstName = "John", Surname = "Wyndham"};

            story1 = new Story { Id = 1, Title = "Some short story" };
            story2 = new Story { Id = 2, Title = "Another story" };
            story3 = new Story { Id = 3, Title = "A totally different short story" };
        }


        [Test]
        public void Will_Map_Publishers_To_Available_Publishers_Dictionary()
        {
            publishers.Add(somePublisher);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePublishers.Count(), Is.EqualTo(1));
            Assert.That(model.AvailablePublishers.First().Value, Is.EqualTo(somePublisher.Id.ToString()));
            Assert.That(model.AvailablePublishers.First().Text, Is.EqualTo(somePublisher.Name));
        }


        [Test]
        public void Will_Set_Null_Publisher_If_Model_Contains_NonExistent_PublisherId()
        {
            var model = new EditBookModel {PublisherId = -1};
            var entity = new Book();

            mapper.MergeEntityWithModel(entity, model);

            Assert.That(entity.Publisher, Is.Null);
        }


        [Test]
        public void Will_Set_Publisher_If_Model_Contains_Some_PublisherId()
        {
            var model = new EditBookModel() { PublisherId = somePublisher.Id };
            var entity = new Book();

            publishers.Add(somePublisher);

            mapper.MergeEntityWithModel(entity, model);

            Assert.That(entity.Publisher, Is.Not.Null);
            Assert.That(entity.Publisher.Id, Is.EqualTo(somePublisher.Id));
        }


        [Test]
        public void Will_Sort_Available_Publishers()
        {
            var bruna = new Publisher {Id = 1, Name = "Bruna"};
            var alpha = new Publisher {Id = 2, Name = "Alpha books"};
            var calda = new Publisher {Id = 3, Name = "Calda ltd."};

            publishers.Add(bruna);
            publishers.Add(alpha);
            publishers.Add(calda);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePublishers.Skip(0).First().Value, Is.EqualTo(alpha.Id.ToString()));
            Assert.That(model.AvailablePublishers.Skip(1).First().Value, Is.EqualTo(bruna.Id.ToString()));
            Assert.That(model.AvailablePublishers.Skip(2).First().Value, Is.EqualTo(calda.Id.ToString()));
        }


        [Test]
        public void Will_Have_Available_People()
        {
            people.Add(asimov);
            people.Add(adams);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePeople.Count(), Is.EqualTo(2));

            var includedIds = model.AvailablePeople.Select(p => p.Value);

            Assert.That(includedIds, Contains.Item(asimov.Id.ToString()));
            Assert.That(includedIds, Contains.Item(adams.Id.ToString()));
        }


        [Test]
        public void Will_Sort_Available_People()
        {
            people.Add(wyndham);
            people.Add(asimov);
            people.Add(adams);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePeople.Skip(0).First().Value, Is.EqualTo(adams.Id.ToString()));
            Assert.That(model.AvailablePeople.Skip(1).First().Value, Is.EqualTo(asimov.Id.ToString()));
            Assert.That(model.AvailablePeople.Skip(2).First().Value, Is.EqualTo(wyndham.Id.ToString()));
        }


        [Test]
        public void Model_Will_Have_Editor_Ids_From_Domain_Book()
        {
            var book = new Book();

            book.AddEditor(asimov);
            book.AddEditor(wyndham);

            var model = mapper.ModelFromEntity(book);

            Assert.That(model.EditorIds.Contains(asimov.Id));
            Assert.That(model.EditorIds.Contains(wyndham.Id));
        }


        [Test]
        public void Domain_Book_Will_Get_Editors_From_Model()
        {
            people.Add(asimov);
            people.Add(wyndham);

            var model = new EditBookModel()
                            {
                                EditorIds = people.Items.Select(p => p.Id).ToArray()
                            };

            var book = new Book();

            mapper.MergeEntityWithModel(book, model);

            Assert.That(book.Editors, Contains.Item(asimov));
            Assert.That(book.Editors, Contains.Item(wyndham));
        }


        [Test]
        public void Will_Throw_When_Provided_With_NonExisting_Editor_Id()
        {
            var model = new EditBookModel { EditorIds = new[] {-42} };

            var book = new Book();

            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(book, model));
        }


        [Test]
        public void Will_Default_Have_Empty_EditorIds_List()
        {
            // This test is here because MVC will instantiate a default model upon
            // posting a form to an action, and the mapper needs an empty list instead
            // of a null value.
            var model = new EditBookModel();
            var book = new Book();
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Editors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Will_Remove_Editors_No_Longer_In_Model()
        {
            var book = new Book();

            book.AddEditor(wyndham);
            book.AddEditor(asimov);

            people.Add(wyndham);
            people.Add(asimov);

            var model = mapper.ModelFromEntity(book);

            // Act! Upon postback only one of the IDs will still be selected:
            model.EditorIds = new[] { asimov.Id };

            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Editors.Count(), Is.EqualTo(1));
            Assert.That(book.Editors.FirstOrDefault(), Is.EqualTo(asimov));
        }


        [Test]
        public void Model_Will_Have_Author_Ids_From_Domain_Book()
        {
            var book = new Book();
            book.AddAuthor(asimov);
            book.AddAuthor(adams);

            var model = mapper.ModelFromEntity(book);

            Assert.That(model.AuthorIds.Contains(asimov.Id));
            Assert.That(model.AuthorIds.Contains(adams.Id));
        }


        [Test]
        public void Domain_Book_Will_Get_Authors_From_Model()
        {
            var book = new Book();
            var model = mapper.ModelFromEntity(book);
            model.AuthorIds = new[] { asimov.Id };
            people.Add(asimov);

            mapper.MergeEntityWithModel(book, model);

            Assert.That(book.Authors, Contains.Item(asimov));
        }


        [Test]
        public void Model_Will_Have_Translator_Ids_From_Domain_Book()
        {
            var book = new Book();
            book.AddTranslator(asimov);
            book.AddTranslator(adams);

            var model = mapper.ModelFromEntity(book);

            Assert.That(model.TranslatorIds.Contains(asimov.Id));
            Assert.That(model.TranslatorIds.Contains(adams.Id));
        }


        [Test]
        public void Domain_Book_Will_Get_Translators_From_Model()
        {
            var book = new Book();
            var model = mapper.ModelFromEntity(book);
            model.TranslatorIds = new[] { asimov.Id };
            people.Add(asimov);

            mapper.MergeEntityWithModel(book, model);

            Assert.That(book.Translators, Contains.Item(asimov));
        }


        [Test]
        public void Model_Will_Have_SubModels_For_Stories()
        {
            var book = new Book();

            book.AddStory(story1);
            book.AddStory(story2);
            book.AddStory(story3);

            var model = mapper.ModelFromEntity(book);

            Assert.That(model.Stories.Skip(0).First().Id, Is.EqualTo(story1.Id));
            Assert.That(model.Stories.Skip(1).First().Id, Is.EqualTo(story2.Id));
            Assert.That(model.Stories.Skip(2).First().Id, Is.EqualTo(story3.Id));
        }


        [Test]
        public void Merge_With_Entity_Will_Synch_Existing_Story_Updates()
        {
            var bookEntity = new Book();
            var storyEntity = new Story() {Id = 1, Title = "Gasp!"};
            bookEntity.AddStory(storyEntity);

            var bookModel = mapper.ModelFromEntity(bookEntity);

            // Act! Change the title
            bookModel.Stories.First().Title = "Gasp, what a Wasp!";

            // Check that the "Act!" was succesful
            Assert.That(bookModel.Stories.First().Title, Is.EqualTo("Gasp, what a Wasp!"));

            mapper.MergeEntityWithModel(bookEntity, bookModel);

            Assert.That(bookEntity.Stories.First().Value.Title, Is.EqualTo("Gasp, what a Wasp!"));
        }


        [Test]
        public void Can_Map_Isbn_Language()
        {
            var book = new Book {Iso639LanguageId = "en"};
            var model = mapper.ModelFromEntity(book);

            Assert.That(model.Iso639LanguageId, Is.EqualTo(book.Iso639LanguageId));
        }


        [Test]
        public void Can_Map_Available_Isbn_Languages()
        {
            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailableIso639Languages, Is.Not.Null);
            Assert.That(model.AvailableIso639Languages, Is.Not.Empty);
        }


        [Test]
        public void Will_Map_Available_Isbn_Languages_From_BookRepository()
        {
            var model = mapper.ModelFromEntity(new Book());

            foreach (var iso639Language in books.Iso639LanguageIdentifiers)
            {
                Assert.That(model.AvailableIso639Languages.Any(item => item.Value == iso639Language.ToString()),
                            "Expected isbn language {0} wasn't found in a list of {1} languages.", iso639Language, model.AvailableIso639Languages.Count());
            }
        }


        [Test]
        public void Will_Throw_Mapping_Exception_If_Model_Refers_To_Nonexistent_Author()
        {
            var book = new Book();
            var model = new EditBookModel { AuthorIds = new[] { 1234 } };
            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(book, model));
        }


        [Test]
        public void Will_Throw_Mapping_Exception_If_Model_Refers_To_Nonexistent_Translator()
        {
            var book = new Book();
            var model = new EditBookModel { TranslatorIds = new[] { 1234 } };
            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(book, model));
        }


        [Test]
        public void Will_Create_New_Story_If_It_Did_Not_Exist_Yet()
        {
            var book = new Book();
            var model = new EditBookModel() {Stories = new[] {new EditStoryModel()}.ToList()};
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Stories.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Create_Model_With_ReviewText()
        {
            var book = new Book { ReviewText = "Great book!" };
            var model = mapper.ModelFromEntity(book);
            Assert.That(model.ReviewText, Is.EqualTo(book.ReviewText));
        }


        [Test]
        public void Can_Merge_ReviewText_Into_Entity()
        {
            var book = new Book();
            var model = new EditBookModel { ReviewText = "Terrible book." };
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.ReviewText, Is.EqualTo(model.ReviewText));
        }


        [Test]
        public void Will_Add_New_Stories_With_Title_Into_Entity()
        {
            var book = new Book();
            var model = new EditBookModel();
            model.NewStories.First().Title = "Great story!";
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Stories.Single().Value.Title, Is.EqualTo("Great story!"));
        }


        [Test]
        public void Will_Allow_Setting_Language_To_Null()
        {
            var book = new Book {Iso639LanguageId = "en"};
            var model = mapper.ModelFromEntity(book);
            model.Iso639LanguageId = null;
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Iso639LanguageId, Is.Null);
        }


        [Test]
        public void Will_Allow_Setting_Publisher_To_Null()
        {
            var book = new Book { Publisher = new Publisher() };
            var model = mapper.ModelFromEntity(book);
            model.PublisherId = null;
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Publisher, Is.Null);
        }


        [Test]
        public void Will_Show_Current_Isbn()
        {
            var book = new Book { Isbn = "123456789" };
            var model = mapper.ModelFromEntity(book);
            Assert.That(model.Isbn, Is.EqualTo(book.Isbn));
        }


        [Test]
        public void Will_Merge_Back_Isbn()
        {
            var book = new Book();
            var model = mapper.ModelFromEntity(book);
            model.Isbn = "123456789";
            mapper.MergeEntityWithModel(book, model);
            Assert.That(book.Isbn, Is.EqualTo(model.Isbn));
        }
    }
}
