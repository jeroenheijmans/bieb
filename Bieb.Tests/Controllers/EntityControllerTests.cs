using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Moq;
using NUnit.Framework;
using PagedList;


namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class EntityControllerTests
    {
        private BookRepositoryMock bookRepository;

        private Mock<HttpResponseBase> responseMock;
        private BooksController booksController;

        private EditBookModelMapper editBookModelMapper;
        private ViewBookModelMapper viewBookModelMapper;

        private IEntityRepository<Publisher> publishersRepository;

        private IEntityRepository<Person> peopleRepository;

        private Book someBook;
        private Book otherBook;
        

        [SetUp]
        public void SetUp()
        {
            bookRepository = new BookRepositoryMock();
            peopleRepository = new RepositoryMock<Person>();

            responseMock = new Mock<HttpResponseBase>();

            publishersRepository = new RepositoryMock<Publisher>();
            editBookModelMapper = new EditBookModelMapper(publishersRepository, peopleRepository, bookRepository, null, new Mock<IIsbnLanguageDisplayer>().Object);
            viewBookModelMapper = new ViewBookModelMapper(new Mock<IIsbnLanguageDisplayer>().Object);

            booksController = new BooksController(bookRepository, viewBookModelMapper, editBookModelMapper, responseMock.Object);

            someBook = new Book
            {
                Id = 42,
                Title = "Hitching Guide"
            };

            otherBook = new Book
            {
                Id = 9000,
                Title = "It's over!"
            };
        }


        [Test]
        public void Will_Guard_Against_Null_For_ViewModelMapper()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new BooksController(new BookRepositoryMock(), null, new Mock<IEditEntityModelMapper<Book, EditBookModel>>().Object);
                });
        }


        [Test]
        public void Will_Guard_Against_Null_For_EditModelMapper()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new BooksController(new BookRepositoryMock(), new Mock<IViewEntityModelMapper<Book, ViewBookModel>>().Object, null);
                });
        }


        [Test]
        public void Can_Get_Item_Details()
        {
            bookRepository.Add(someBook);

            var result = booksController.Details(someBook.Id);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<ViewBookModel>());
            Assert.That(((ViewBookModel)vResult.Model).Id, Is.EqualTo(someBook.Id));
        }


        [Test]
        public void Can_List_All_Items()
        {
            bookRepository.Add(someBook);
            bookRepository.Add(someBook);
            bookRepository.Add(someBook);

            var result = booksController.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<ViewBookModel>>());
            Assert.That(((IEnumerable<ViewBookModel>)vResult.Model).Count(), Is.EqualTo(3));
        }


        [Test]
        public void Can_Start_Creating_New_Item()
        {
            var result = booksController.Create();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<EditBookModel>());
            Assert.That(((EditBookModel)vResult.Model).Id, Is.EqualTo(0));
        }


        [Test]
        public void Can_Create_New_Item()
        {
            var newBookModel = new EditBookModel();

            var result = booksController.Create(newBookModel);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues.ContainsKey("id"));
        }


        [Test]
        public void Can_Delete_Item()
        {
            bookRepository.Add(someBook);
            booksController.Delete(someBook.Id);
            Assert.That(bookRepository.GetItem(someBook.Id), Is.Null);
        }


        [Test]
        public void Delete_Will_Return_RedirectResult()
        {
            bookRepository.Add(someBook);
            var result = booksController.Delete(someBook.Id);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Delete_Will_Redirect_To_Index()
        {
            bookRepository.Add(someBook);
            var result = (RedirectToRouteResult)booksController.Delete(someBook.Id);
            Assert.That(result.RouteValues["Action"], Is.EqualTo("Index"));
        }


        [Test]
        public void Index_Will_Have_Default_Page_Size_Of_25_On_PageNumber_1()
        {
            for (int i = 0; i < 100; i++)
            {
                bookRepository.Add(new Book());
            }

            var result = booksController.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = ((ViewResult)result).Model as StaticPagedList<ViewBookModel>;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.PageNumber, Is.EqualTo(1));
            Assert.That(model.PageSize, Is.EqualTo(25));
            Assert.That(model.Count, Is.EqualTo(25));
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

            bookRepository.Add(someBook);
            bookRepository.Add(otherBook);

            var result = booksController.RecentlyAdded();

            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var book = ((PartialViewResult)result).Model as ViewBookModel;

            Assert.That(book, Is.Not.Null);
            Assert.That(book.Id, Is.EqualTo(1));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Sort_By_CreatedDate_ThenBy_Id()
        {
            var bulkInsertDate = new DateTime(2013, 01, 10);

            var book1 = new Book { Title = "Zoltan the Great", Id = 1, CreatedDate = bulkInsertDate };
            var book2 = new Book { Title = "Middle-man", Id = 2, CreatedDate = bulkInsertDate };

            bookRepository.Add(book1);
            bookRepository.Add(book2);

            var result = booksController.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var book = ((PartialViewResult)result).Model as ViewBookModel;

            Assert.That(book, Is.Not.Null);
            Assert.That(book.Id, Is.EqualTo(2));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Return_Null_If_Repository_Is_Empty()
        {
            var result = booksController.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var vresult = (PartialViewResult)result;

            Assert.That(vresult.Model, Is.Null);
        }

        
        [Test]
        public void Save_Action_Will_Call_NotifyChanges_In_Repository()
        {
            var book = new Book { Id = 42, ModifiedDate = DateTime.Now };
            var model = editBookModelMapper.ModelFromEntity(book);

            var mock = new Mock<IEntityRepository<Book>>();
            var controller = new BooksController(mock.Object, viewBookModelMapper, editBookModelMapper);

            mock.Setup(repo => repo.GetItem(book.Id)).Returns(book);

            controller.Save(model);

            mock.Verify(repo => repo.NotifyItemWasChanged(book));
        }


        [Test]
        public void Save_Action_Will_Redirect_To_Details_Action()
        {
            var mock = new Mock<IEntityRepository<Book>>();
            var controller = new BooksController(mock.Object, viewBookModelMapper, editBookModelMapper);

            var book = new Book { Id = 1 };
            var model = editBookModelMapper.ModelFromEntity(book);
            
            mock.Setup(repo => repo.GetItem(1)).Returns(book);

            var result = controller.Save(model);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirectResult.RouteValues["id"], Is.EqualTo(1));
        }


        [Test]
        public void Save_Action_Will_Redirect_To_Details_For_New_Item()
        {
            var model = new EditBookModel {Id = 0};

            var result = booksController.Save(model);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Edit_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)booksController.Edit(123456789);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }


        [Test]
        public void Details_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)booksController.Details(123456789);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }

        [Test]
        public void Delete_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)booksController.Delete(123456);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }


        [Test]
        public void Http404_Will_Set_Response_Code()
        {
            responseMock.SetupProperty(response => response.StatusCode);
            booksController.PageNotFound();
            Assert.That(responseMock.Object.StatusCode, Is.EqualTo(404));
        }
    }
}