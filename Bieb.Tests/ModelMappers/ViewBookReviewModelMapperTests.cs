using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewBookReviewModelMapperTests
    {
        private IViewEntityModelMapper<Review<Book>, ViewBookReviewModel> mapper;


        [SetUp]
        public void SetUp()
        {
            mapper = new ViewBookReviewModelMapper();
        }


        [Test]
        public void Can_Map_Subject_AsLinkableBookModel()
        {
            var review = new Review<Book> {Subject = new Book {Id = 42}};
            var model = mapper.ModelFromEntity(review);
            Assert.That(model.Book.Id, Is.EqualTo(review.Subject.Id));
        }
    }
}
