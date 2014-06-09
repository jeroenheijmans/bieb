using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private IEntityRepository<Book> bookRepository;
        private IEntityRepository<Person> peopleRepository;
        private SurpriseController controller;
        private Book testBook;
        private Person testPerson;


        [SetUp]
        public void SetUp()
        {
            peopleRepository = new RepositoryMock<Person>();
            bookRepository = new RepositoryMock<Book>();

            controller = new SurpriseController(peopleRepository, bookRepository);
            testBook = new Book();
            testPerson = new Person();
        }


        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_Books_In_Database()
        {
            Debug.Assert(!bookRepository.Items.Any(), "Repository must be empty for this test to work.");
            ActionResult result = controller.Index();
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_People_In_Database()
        {
            Debug.Assert(!peopleRepository.Items.Any(), "Repository must be empty for this test to work.");
            ActionResult result = controller.Index();
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Index_Will_Redirect_To_Books_Controller_For_Surprise_Books()
        {
            peopleRepository.Add(testPerson);
            bookRepository.Add(testBook);

            var randomEntityPickerMock = new Mock<IRandomEntityPicker>();
            randomEntityPickerMock.Setup(picker => picker.GetRandomEntityType()).Returns(typeof(Book));

            var result = (RedirectToRouteResult)controller.RandomItem(randomEntityPickerMock.Object);

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
        }


        [Test]
        public void Index_Will_Redirect_To_People_Controller_For_Surprise_Person()
        {
            peopleRepository.Add(testPerson);
            bookRepository.Add(testBook);

            var randomEntityPickerMock = new Mock<IRandomEntityPicker>();
            randomEntityPickerMock.Setup(picker => picker.GetRandomEntityType()).Returns(typeof(Person));

            var result = (RedirectToRouteResult)controller.RandomItem(randomEntityPickerMock.Object);

            Assert.That(result.RouteValues["controller"], Is.EqualTo("People"));
        }
    }
}
