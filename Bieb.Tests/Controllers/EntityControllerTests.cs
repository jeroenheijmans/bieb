using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Moq;
using NUnit.Framework;
using PagedList;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class EntityControllerTests
    {
        [Test]
        public void Can_Get_Item_Details()
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
        public void Can_List_All_Items()
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

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<LibraryBook>>());

            var LibraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(LibraryBookList.PageNumber, Is.EqualTo(1));
            Assert.That(LibraryBookList.PageSize, Is.EqualTo(25));
            Assert.That(LibraryBookList.Count, Is.EqualTo(25));
        }

        [Test]
        public void RecentlyAdded_Action_Will_Sort_By_CreatedDate()
        {
            // Arrange
            var insertionDateEarly = new DateTime(2013, 01, 10);
            var insertionDateLate = new DateTime(2013, 01, 25);

            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var LibraryBook1 = new LibraryBook { Title = "Zoltan the Great", Id = 1, CreatedDate = insertionDateLate };

            // Higher ("later") Id value, but earlier CreatedDate
            var LibraryBook2 = new LibraryBook { Title = "Middle-man", Id = 2, CreatedDate = insertionDateEarly };

            mock.Setup(repo => repo.Items).Returns((new[] { LibraryBook1, LibraryBook2 }).AsQueryable());
            var controller = new LibraryBooksController(mock.Object);

            // Act & Assert
            ActionResult result = controller.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<LibraryBook>());

            var LibraryBook = (LibraryBook)vresult.Model;

            Assert.That(LibraryBook.Id, Is.EqualTo(1));
        }

        [Test]
        public void RecentlyAdded_Action_Will_Sort_By_CreatedDate_ThenBy_Id()
        {
            // Arrange
            var bulkInsertDate = new DateTime(2013, 01, 10);

            var mock = new Mock<IEntityRepository<LibraryBook>>();
            var LibraryBook1 = new LibraryBook { Title = "Zoltan the Great", Id = 1, CreatedDate = bulkInsertDate };
            var LibraryBook2 = new LibraryBook { Title = "Middle-man", Id = 2, CreatedDate = bulkInsertDate };

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
        public void RecentlyAdded_Action_Will_Return_Null_If_Repository_Is_Empty()
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
