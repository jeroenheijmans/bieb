using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class BookControllerTest
    {
        [Test]
        public void Can_Get_Book_Details()
        {
            // Arrange
            Mock<IEntityRepository<Book>> mock = new Mock<IEntityRepository<Book>>();
            Book myBook = new Book() { Id = 42 };
            mock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(() => myBook);

            // Act
            BookController controller = new BookController(mock.Object);
            ActionResult result = controller.Details(23);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.InstanceOf(typeof(Book)));
            Assert.That((vResult.Model as Book).Id, Is.EqualTo(42));
        }

        [Test]
        public void Can_List_All_Books()
        {
            // Arrange
            Mock<IEntityRepository<Book>> mock = new Mock<IEntityRepository<Book>>();
            IQueryable<Book> books = (new Book[] { new Book() { Id = 1 }, new Book() { Id = 2 }, new Book() { Id = 3 } }).AsQueryable<Book>();
            mock.Setup(repo => repo.Items).Returns(books);

            // Act
            BookController controller = new BookController(mock.Object);
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.InstanceOf(typeof(IEnumerable<Book>)));
            Assert.That((vResult.Model as IEnumerable<Book>).Count(), Is.EqualTo(3));
        }
    }
}
