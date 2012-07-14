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
            mock.Setup(repo => repo.GetItem(It.IsAny<int>())).Returns(() => myBook);

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
            BookController controller = new BookController(mock.Object);

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            ViewResult vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<Book>>());
            Assert.That((vResult.Model as IEnumerable<Book>).Count(), Is.EqualTo(3));
        }

        [Test]
        public void Can_Start_Creating_New_Item()
        {
            // Arrange
            Mock<IEntityRepository<Book>> mock = new Mock<IEntityRepository<Book>>();
            BookController controller = new BookController(mock.Object);

            // Act
            ActionResult result = controller.Create();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            ViewResult vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.InstanceOf<Book>());
            Assert.That((vResult.Model as Book).Id, Is.EqualTo(default(int)));
        }

        [Test]
        public void Can_Create_New_Item()
        {
            // Arrange
            Mock<IEntityRepository<Book>> mock = new Mock<IEntityRepository<Book>>();

            // TODO: Refactor this Save mock method so it can be reused.
            mock.Setup(repo => repo.Save(It.IsAny<Book>())).Returns(
                    (Book target) =>
                    {
                        if (target.Id == default(int))
                        {
                            // New object
                            target.Id = 1;
                            target.Version = 1;
                        }
                        else
                        {
                            // Save existing object
                            target.Version++;
                        }
                        return target;
                    }
                );
            BookController controller = new BookController(mock.Object);
            Book newBook = new Book() { Title = "Lord of the Flies" };

            // Act
            ActionResult result = controller.Create(newBook);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;
            Assert.That(redirectResult.RouteValues.ContainsKey("id"));
            Assert.That(redirectResult.RouteValues["id"], Is.Not.EqualTo(default(int)));
        }
        
    }
}
