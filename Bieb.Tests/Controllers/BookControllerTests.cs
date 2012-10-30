using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using PagedList;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class BookControllerTests
    {
        [Test]
        public void Can_Get_Book_Details()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();
            var myBook = new Book { Id = 42 };
            mock.Setup(repo => repo.GetItem(It.IsAny<int>())).Returns(() => myBook);

            // Act
            var controller = new BookController(mock.Object);
            ActionResult result = controller.Details(23);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf(typeof(Book)));
            Assert.That(((Book)vResult.Model).Id, Is.EqualTo(42));
        }

        [Test]
        public void Can_List_All_Books()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();
            mock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(new Book(), 3).AsQueryable());
            var controller = new BookController(mock.Object);

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<Book>>());
            Assert.That(((IEnumerable<Book>)vResult.Model).Count(), Is.EqualTo(3));
        }

        [Test]
        public void Can_Start_Creating_New_Item()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();
            var controller = new BookController(mock.Object);

            // Act
            ActionResult result = controller.Create();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<Book>());
            Assert.That(((Book)vResult.Model).Id, Is.EqualTo(default(int)));
        }

        [Test]
        public void Can_Create_New_Item()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();

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
            var controller = new BookController(mock.Object);
            var newBook = new Book { Title = "Lord of the Flies" };

            // Act
            ActionResult result = controller.Create(newBook);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues.ContainsKey("id"));
            Assert.That(redirectResult.RouteValues["id"], Is.Not.EqualTo(default(int)));
        }

        [Test]
        public void Index_Will_Have_Default_Page_Size_Of_25_On_PageNumber_1()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();
            mock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(new Book(), 100).AsQueryable());
            var controller = new BookController(mock.Object);

            // Act & Assert
            ActionResult result = controller.Index();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            
            var vresult = (ViewResult)result;
            
            Assert.That(vresult.Model, Is.InstanceOf <PagedList<Book>>());

            var bookList = (PagedList<Book>)vresult.Model;

            Assert.That(bookList.PageNumber, Is.EqualTo(1));
            Assert.That(bookList.PageSize, Is.EqualTo(25));
            Assert.That(bookList.Count, Is.EqualTo(25));
        }
        
        [Test]
        public void Index_Will_Have_Default_TitleSort_Sorting()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<Book>>();
            var book1 = new Book { Title = "Zoltan the Great" };
            var book2 = new Book { Title = "Middle-man" };
            var book3 = new Book { Title = "Alpha came before Omega" };
            mock.Setup(repo => repo.Items).Returns((new[] { book1, book2, book3 }).AsQueryable());
            var controller = new BookController(mock.Object);

            // Act & Assert
            ActionResult result = controller.Index();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<Book>>());

            var bookList = (PagedList<Book>)vresult.Model;

            Assert.That(bookList[0], Is.EqualTo(book3));
            Assert.That(bookList[1], Is.EqualTo(book2));
            Assert.That(bookList[2], Is.EqualTo(book1));
        }
    }
}
