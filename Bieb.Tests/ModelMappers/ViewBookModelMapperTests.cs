using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models.Books;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewBookModelMapperTests
    {
        private ViewBookModelMapper mapper;


        [SetUp]
        public void SetUp()
        {
            mapper = new ViewBookModelMapper(new Iso639LanguageDisplayer());
        }


        [Test]
        public void Will_Show_Comma_Separated_Tags()
        {
            var book = new Book();
            book.AddTag(new Tag("SF"));
            book.AddTag(new Tag("Fantasy"));

            var result = mapper.ModelFromEntity(book);

            Assert.That(result.Tags, Is.Not.Null.Or.Empty);
            Assert.That(result.Tags, Is.EqualTo("SF, Fantasy"));
        }


        [Test]
        public void Will_Show_Publishing_Info_If_Publisher_Is_Set()
        {
            var book = new Book { Publisher = new Publisher() };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_Info_If_Year_Is_Set()
        {
            var book = new Book { Year = 2001 };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_Info_If_Language_Is_Set()
        {
            var book = new Book { Iso639LanguageId = "nl" };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_If_Year_And_Language_Are_Set()
        {
            var book = new Book { Year = 2001, Iso639LanguageId = "nl" };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_If_Year_And_Publisher_Are_Set()
        {
            var book = new Book { Year = 2001, Publisher = new Publisher() };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_If_Language_And_Publisher_Are_Set()
        {
            var book = new Book { Iso639LanguageId = "nl", Publisher = new Publisher() };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Can_Include_Review()
        {
            var book = new Book { ReviewText = "Good book!!" };

            var result = mapper.ModelFromEntity(book);

            Assert.That(result.ReviewText, Is.EqualTo(book.ReviewText));
        }


        [Test]
        public void Will_Show_Stories_If_Book_Has_Multiple_Stories()
        {
            var book = new Book();
            book.AddStory(new Story());
            book.AddStory(new Story());
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowStoriesList);
        }


        [Test]
        public void Has_List_Of_Stories()
        {
            var book = new Book();
            book.AddStory(new Story() { Title = "Raven" });
            book.AddStory(new Story() { Title = "Hammerfall" });

            var result = mapper.ModelFromEntity(book);

            Assert.That(result.Stories.First().Title, Is.EqualTo(book.Stories.First().Value.Title));
            Assert.That(result.Stories.Second().Title, Is.EqualTo(book.Stories.Second().Value.Title));
        }


        [Test]
        public void Will_Map_All_Editors()
        {
            var book = new Book();
            book.AddEditor(new Person());
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.Editors.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Map_All_Authors()
        {
            var book = new Book();
            var person = new Person();
            book.AddAuthor(person);
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.Authors.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Map_All_Translators()
        {
            var book = new Book();
            book.AddTranslator(new Person());
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.Translators.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Map_ReferenceBook()
        {
            var book = new Book {ReferenceBook = new Book {Id = 42}};
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ReferenceBook.Id, Is.EqualTo(book.ReferenceBook.Id));
        }


        [Test]
        public void Will_Map_Editors_As_CoverPeople_If_Available()
        {
            var book = new Book();
            book.AddEditor(new Person { Id = 42 });
            var model = mapper.ModelFromEntity(book);
            Assert.That(model.CoverPeople.First().Id, Is.EqualTo(42));
        }


        [Test]
        public void Will_Map_Authors_As_CoverPeople_If_No_Editors_Available()
        {
            var book = new Book();
            var person = new Person {Surname = "Asimov"};
            book.AddAuthor(person);
            var model = mapper.ModelFromEntity(book);
            Assert.That(model.CoverPeople.First().Text, Is.StringContaining(person.Surname));
        }


        [Test]
        public void Will_Map_Series()
        {
            var book = new Book {Series = new Series {Id = 42}};
            var model = mapper.ModelFromEntity(book);
            Assert.That(model.Series.Id, Is.EqualTo(book.Series.Id));
        }
    }
}
