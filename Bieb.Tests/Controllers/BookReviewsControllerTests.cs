using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class BookReviewsControllerTests
    {
        private IEntityRepository<Review<Book>> repository;
        private BookReviewsController controller;


        [SetUp]
        public void SetUp()
        {
            repository = new RepositoryMock<Review<Book>>();

            var viewMapper = new ViewBookReviewModelMapper();
            var editMapper = new Mock<IEditEntityModelMapper<Review<Book>, EditBookReviewModel>>();

            controller = new BookReviewsController(repository, viewMapper, editMapper.Object);
        }


        [Test]
        public void Index_Will_Sort_By_Rating_Descending()
        {
            repository.Add(new Review<Book> { Id = 1, Rating = 5 });
            repository.Add(new Review<Book> { Id = 2, Rating = 10 });

            var result = (ViewResult)controller.Index();
            var model = (IEnumerable<ViewBookReviewModel>)result.Model;

            Assert.That(model.First().Id, Is.EqualTo(2));
            Assert.That(model.Second().Id, Is.EqualTo(1));
        }
    }
}
