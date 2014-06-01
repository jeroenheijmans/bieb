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
            mapper = new ViewBookModelMapper(new IsbnLanguageDisplayer());
        }


        [Test]
        public void Will_Show_Comma_Separated_Tags()
        {
            var book = new Book();
            book.Tags.Add(new Tag("SF"));
            book.Tags.Add(new Tag("Fantasy"));

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
            var book = new Book { IsbnLanguage = 90 };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Will_Show_Publishing_If_Year_And_Language_Are_Set()
        {
            var book = new Book { Year = 2001, IsbnLanguage = 90 };
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
            var book = new Book { IsbnLanguage = 3, Publisher = new Publisher() };
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowPublishingInfo);
        }


        [Test]
        public void Can_Include_Reviews()
        {
            var review = new Review<Book>() {Rating = 8, ReviewText = "Sublime!"};
            var book = new Book();
            book.Reviews.Add(review);

            var result = mapper.ModelFromEntity(book);

            Assert.That(result.Reviews.Count() == 1, "There should be exactly one review in the model.");
            Assert.That(result.Reviews.First().Text == review.ReviewText, "Review text should come from entity.");
            Assert.That(result.Reviews.First().Rating == review.Rating, "Rating should come from entity.");
        }


        [Test]
        public void Will_Show_Stories_If_Book_Has_Multiple_Stories()
        {
            var book = new Book();
            book.Stories.Add(1, new Story());
            book.Stories.Add(2, new Story());
            var result = mapper.ModelFromEntity(book);
            Assert.That(result.ShowStoriesList);
        }


        [Test]
        public void Has_List_Of_Stories()
        {
            var book = new Book();
            book.Stories.Add(0, new Story() { Title = "Raven" });
            book.Stories.Add(1, new Story() { Title = "Hammerfall" });

            var result = mapper.ModelFromEntity(book);

            Assert.That(result.Stories.First().Title, Is.EqualTo(book.Stories.First().Value.Title));
            Assert.That(result.Stories.Second().Title, Is.EqualTo(book.Stories.Second().Value.Title));
        }
    }
}
