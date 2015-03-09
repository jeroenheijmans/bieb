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
using Bieb.Web.Models.Publishers;
using Moq;
using NUnit.Framework;
using PagedList;


namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class EntityControllerTests
    {
        private BookRepositoryMock repository;

        private Mock<HttpResponseBase> responseMock;
        private BooksController controller;

        private EditBookModelMapper editBookModelMapper;
        private ViewBookModelMapper viewBookModelMapper;

        private Book someBook;
        private Book otherBook;
        

        [SetUp]
        public void SetUp()
        {
            repository = new BookRepositoryMock();

            responseMock = new Mock<HttpResponseBase>();

            var peopleRepository = new RepositoryMock<Person>();
            var publishersRepository = new RepositoryMock<Publisher>();

            editBookModelMapper = new EditBookModelMapper(publishersRepository, peopleRepository, repository, null, new Mock<IIso639LanguageDisplayer>().Object);
            viewBookModelMapper = new ViewBookModelMapper(new Mock<IIso639LanguageDisplayer>().Object);

            controller = new BooksController(repository, viewBookModelMapper, editBookModelMapper, responseMock.Object);

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
        public void Can_Create_Basic_Controller()
        {
            // Smoke test
            Assert.DoesNotThrow(() => new BaseController());
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
            repository.Add(someBook);

            var result = controller.Details(someBook.Id);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<ViewBookModel>());
            Assert.That(((ViewBookModel)vResult.Model).Id, Is.EqualTo(someBook.Id));
        }


        [Test]
        public void Can_List_All_Items()
        {
            repository.Add(someBook);
            repository.Add(someBook);
            repository.Add(someBook);

            var result = controller.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<IEnumerable<ViewBookModel>>());
            Assert.That(((IEnumerable<ViewBookModel>)vResult.Model).Count(), Is.EqualTo(3));
        }


        [Test]
        public void Controller_Without_IndexFilter_Will_Use_Default_Func()
        {
            var simpleController = new PublishersController(
                new RepositoryMock<Publisher>(new[] { new Publisher() }),
                new Mock<IViewEntityModelMapper<Publisher, ViewPublisherModel>>().Object,
                new Mock<EditPublisherModelMapper>().Object);

            var result = (ViewResult)simpleController.Index();
            Assert.That(result.Model, Is.InstanceOf<IEnumerable<ViewPublisherModel>>());
            Assert.That(((IEnumerable<ViewPublisherModel>)result.Model).Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Start_Creating_New_Item()
        {
            var result = controller.Create();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var vResult = (ViewResult)result;
            Assert.That(vResult.Model, Is.InstanceOf<EditBookModel>());
            Assert.That(((EditBookModel)vResult.Model).Id, Is.EqualTo(0));
        }


        [Test]
        public void Can_Create_New_Item()
        {
            var newBookModel = new EditBookModel();

            var result = controller.Create(newBookModel);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues.ContainsKey("id"));
        }


        [Test]
        public void Can_Delete_Item()
        {
            repository.Add(someBook);
            controller.Delete(someBook.Id);
            Assert.That(repository.GetItem(someBook.Id), Is.Null);
        }


        [Test]
        public void Delete_Will_Return_RedirectResult()
        {
            repository.Add(someBook);
            var result = controller.Delete(someBook.Id);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Delete_Will_Redirect_To_Index()
        {
            repository.Add(someBook);
            var result = (RedirectToRouteResult)controller.Delete(someBook.Id);
            Assert.That(result.RouteValues["Action"], Is.EqualTo("Index"));
        }


        [Test]
        public void Index_Will_Have_Default_Page_Size_Of_25_On_PageNumber_1()
        {
            for (int i = 0; i < 100; i++)
            {
                repository.Add(new Book());
            }

            var result = controller.Index();

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

            repository.Add(someBook);
            repository.Add(otherBook);

            var result = controller.RecentlyAdded();

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

            repository.Add(book1);
            repository.Add(book2);

            var result = controller.RecentlyAdded();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var book = ((PartialViewResult)result).Model as ViewBookModel;

            Assert.That(book, Is.Not.Null);
            Assert.That(book.Id, Is.EqualTo(2));
        }


        [Test]
        public void RecentlyAdded_Action_Will_Return_Null_If_Repository_Is_Empty()
        {
            var result = controller.RecentlyAdded();
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

            var result = controller.Save(model);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirectResult = (RedirectToRouteResult)result;
            Assert.That(redirectResult.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Save_Will_Return_Edit_View_If_State_Invalid()
        {
            controller.ModelState.AddModelError("", "");
            var model = new EditBookModel {Id = 13};
            var result = (ViewResult)controller.Save(model);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.Model, Is.EqualTo(model));
        }


        [Test]
        public void Edit_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)controller.Edit(123456789);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }


        [Test]
        public void Edit_Will_Call_Return_View_With_Mapped_Model()
        {
            repository.Add(someBook);
            var result = controller.Edit(someBook.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<EditBookModel>());
        }


        [Test]
        public void Details_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)controller.Details(123456789);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }


        [Test]
        public void Delete_Will_Give_Throw_HttpError_404_On_Id_Not_Found()
        {
            var result = (ViewResult)controller.Delete(123456);
            Assert.That(result.ViewName, Is.EqualTo("PageNotFound"));
        }


        [Test]
        public void Http404_Will_Set_Response_Code()
        {
            responseMock.SetupProperty(response => response.StatusCode);
            controller.PageNotFound();
            Assert.That(responseMock.Object.StatusCode, Is.EqualTo(404));
        }
    }
}