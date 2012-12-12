using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using PagedList;
using System;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        [Test]
        public void Can_Get_LibraryBook_Details()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var myLibraryBook = new LibraryBook { Id = 42 };
            mock.Setup(repo => repo.GetItem(It.IsAny<int>())).Returns(() => myLibraryBook);

            // Act
            var controller = new LibraryBooksController(mock.Object);
            ActionResult result = controller.Details(23);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf(typeof(LibraryBook)));
            Assert.That(((LibraryBook)vResult.Model).Id, Is.EqualTo(42));
        }

        [Test]
        public void Can_List_All_LibraryBooks()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            mock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(new LibraryBook(), 3).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<LibraryBook>>());
            Assert.That(((IEnumerable<LibraryBook>)vResult.Model).Count(), Is.EqualTo(3));
        }

        [Test]
        public void Can_Start_Creating_New_Item()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var controller = new LibraryBooksController(mock.Object);

            // Act
            ActionResult result = controller.Create();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<LibraryBook>());
            Assert.That(((LibraryBook)vResult.Model).Id, Is.EqualTo(default(int)));
        }

        [Test]
        public void Can_Create_New_Item()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();

            // TODO: Refactor this Save mock method so it can be reused.
            mock.Setup(repo => repo.Save(It.IsAny<LibraryBook>())).Returns(
                    (LibraryBook target) =>
                    {
                        if (target.Id == default(int))
                        {
                            // New object
                            target.Id = 1;
                            target.ModifiedDate = target.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            // Save existing object
                            target.ModifiedDate = DateTime.Now;
                        }
                        return target;
                    }
                );
            var controller = new LibraryBooksController(mock.Object);
            var newLibraryBook = new LibraryBook { Title = "Lord of the Flies" };

            // Act
            ActionResult result = controller.Create(newLibraryBook);

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
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            mock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(new LibraryBook(), 100).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act & Assert
            ActionResult result = controller.Index();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            
            var vresult = (ViewResult)result;
            
            Assert.That(vresult.Model, Is.InstanceOf <PagedList<LibraryBook>>());

            var LibraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(LibraryBookList.PageNumber, Is.EqualTo(1));
            Assert.That(LibraryBookList.PageSize, Is.EqualTo(25));
            Assert.That(LibraryBookList.Count, Is.EqualTo(25));
        }
        
        [Test]
        public void Index_Will_Have_Default_TitleSort_Sorting()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var LibraryBook1 = new LibraryBook { Title = "Zoltan the Great" };
            var LibraryBook2 = new LibraryBook { Title = "Middle-man" };
            var LibraryBook3 = new LibraryBook { Title = "Alpha came before Omega" };
            mock.Setup(repo => repo.Items).Returns((new[] { LibraryBook1, LibraryBook2, LibraryBook3 }).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act & Assert
            ActionResult result = controller.Index();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<LibraryBook>>());

            var LibraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(LibraryBookList[0], Is.EqualTo(LibraryBook3));
            Assert.That(LibraryBookList[1], Is.EqualTo(LibraryBook2));
            Assert.That(LibraryBookList[2], Is.EqualTo(LibraryBook1));
        }

        [Test]
        public void RecentlyAdded_Action_Will_Give_LibraryBook_With_Newest_ID()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var LibraryBook1 = new LibraryBook { Title = "Zoltan the Great", Id = 1 };
            var LibraryBook2 = new LibraryBook { Title = "Middle-man", Id = 2 };
            mock.Setup(repo => repo.Items).Returns((new[] { LibraryBook1, LibraryBook2 }).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act & Assert
            ActionResult result = controller.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<LibraryBook>());

            var LibraryBook = (LibraryBook)vresult.Model;

            Assert.That(LibraryBook.Id, Is.EqualTo(2));
        }

        [Test]
        public void RecentlyAdded_Action_Will_Return_Null_If_No_LibraryBooks_Are_In_Repository()
        {
            // Arrange
            var mock = new Mock<IEntityRepository<LibraryBook>>();
            mock.Setup(repo => repo.Items).Returns((new LibraryBook[] { }).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act & Assert
            ActionResult result = controller.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.Null);
        }
    }
}
