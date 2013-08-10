using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Bieb.Web.Models;
using Moq;
using NUnit.Framework;
using PagedList;


namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class EntityControllerTests
    {
        private Mock<IEntityRepository<LibraryBook>> bookRepositoryMock;
        private LibraryBooksController booksController;
        private LibraryBook someBook;
        private LibraryBook otherBook;
        

        [SetUp]
        public void SetUp()
        {
            bookRepositoryMock = new Mock<IEntityRepository<LibraryBook>>();
            booksController = new LibraryBooksController(bookRepositoryMock.Object);

            someBook = new LibraryBook
            {
                Id = 42,
                Title = "Hitching Guide"
            };

            otherBook = new LibraryBook
            {
                Id = 9000,
                Title = "It's over!"
            };
        }


        [Test]
        public void Can_Get_Item_Details()
        {
            bookRepositoryMock.Setup(repo => repo.GetItem(It.IsAny<int>())).Returns(() => someBook);

            var result = booksController.Details(someBook.Id);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<LibraryBook>());
            Assert.That(((LibraryBook)vResult.Model).Id, Is.EqualTo(someBook.Id));
        }


        [Test]
        public void Can_List_All_Items()
        {
            bookRepositoryMock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(someBook, 3).AsQueryable());

            var result = booksController.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<LibraryBook>>());
            Assert.That(((IEnumerable<LibraryBook>)vResult.Model).Count(), Is.EqualTo(3));
        }


        [Test]
        public void Can_Start_Creating_New_Item()
        {
            var result = booksController.Create();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<LibraryBook>());
            Assert.That(((LibraryBook)vResult.Model).Id, Is.EqualTo(0));
        }


        [Test]
        public void Can_Create_New_Item()
        {
            var result = booksController.Create(someBook);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues.ContainsKey("id"));
            Assert.That(redirectResult.RouteValues["id"], Is.Not.EqualTo(0));
        }


        [Test]
        public void Index_Will_Have_Default_Page_Size_Of_25_On_PageNumber_1()
        {
            bookRepositoryMock.Setup(repo => repo.Items).Returns(Enumerable.Repeat(new LibraryBook(), 100).AsQueryable());

            var result = booksController.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<LibraryBook>>());

            var libraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(libraryBookList.PageNumber, Is.EqualTo(1));
            Assert.That(libraryBookList.PageSize, Is.EqualTo(25));
            Assert.That(libraryBookList.Count, Is.EqualTo(25));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Sort_By_CreatedDate()
        {
            var insertionDateEarly = new DateTime(2013, 01, 10);
            var insertionDateLate = new DateTime(2013, 01, 25);

            someBook.Id = 1;
            someBook.CreatedDate = insertionDateLate;

            // Higher ("later") Id value, but earlier CreatedDate
            otherBook.Id = 2;
            otherBook.CreatedDate = insertionDateEarly;

            bookRepositoryMock.Setup(repo => repo.Items).Returns((new[] { someBook, otherBook }).AsQueryable());

            var result = booksController.RecentlyAdded();

            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<LibraryBook>());

            var libraryBook = (LibraryBook)vresult.Model;

            Assert.That(libraryBook.Id, Is.EqualTo(1));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Sort_By_CreatedDate_ThenBy_Id()
        {
            var bulkInsertDate = new DateTime(2013, 01, 10);

            var libraryBook1 = new LibraryBook { Title = "Zoltan the Great", Id = 1, CreatedDate = bulkInsertDate };
            var libraryBook2 = new LibraryBook { Title = "Middle-man", Id = 2, CreatedDate = bulkInsertDate };

            bookRepositoryMock.Setup(repo => repo.Items).Returns((new[] { libraryBook1, libraryBook2 }).AsQueryable());

            var result = booksController.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<LibraryBook>());

            var libraryBook = (LibraryBook)vresult.Model;

            Assert.That(libraryBook.Id, Is.EqualTo(2));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Return_Null_If_Repository_Is_Empty()
        {
            bookRepositoryMock.Setup(repo => repo.Items).Returns((new LibraryBook[] { }).AsQueryable());

            var result = booksController.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.Null);
        }

        
        [Test]
        public void Save_Action_Will_Call_NotifyChanges_In_Repository()
        {
            var person = new Person();
            var viewModel = new PersonModel(person);
            var mock = new Mock<IEntityRepository<Person>>();
            var controller = new PeopleController(mock.Object);

            mock.Setup(repo => repo.GetItem(0)).Returns(person);

            controller.Save(viewModel);

            mock.Verify(repo => repo.NotifyItemWasChanged(person));
        }


        [Test]
        public void Save_Action_Will_Redirect_To_Details_Action()
        {
            var mock = new Mock<IEntityRepository<Person>>();
            var controller = new PeopleController(mock.Object);
            var person = new Person { Id = 1 };
            var viewModel = new PersonModel(person);
            mock.Setup(repo => repo.GetItem(1)).Returns(person);

            var result = controller.Save(viewModel);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirectResult.RouteValues["id"], Is.EqualTo(1));
        }
    }
}