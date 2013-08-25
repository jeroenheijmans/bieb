using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class SurpriseControllerTests
    {
        private IEntityRepository<LibraryBook> bookRepository;
        private IEntityRepository<Person> peopleRepository;
        private SurpriseController controller;
        private LibraryBook testBook;
        private Person testPerson;


        [SetUp]
        public void SetUp()
        {
            peopleRepository = new RepositoryMock<Person>();
            bookRepository = new RepositoryMock<LibraryBook>();

            controller = new SurpriseController(peopleRepository, bookRepository);
            testBook = new LibraryBook();
            testPerson = new Person();
        }


        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_Books_In_Database()
        {
            ActionResult result = controller.Index();

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Index_Will_Redirect_To_Books_Controller_For_Surprise_Books()
        {
            peopleRepository.Add(testPerson);
            bookRepository.Add(testBook);
            
            var randomEntityPickerMock = new Mock<IRandomEntityPicker>();
            randomEntityPickerMock.Setup(picker => picker.GetRandomEntityType()).Returns(typeof(LibraryBook));

            var result = (RedirectToRouteResult)controller.Index(randomEntityPickerMock.Object);

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
        }
    }
}
