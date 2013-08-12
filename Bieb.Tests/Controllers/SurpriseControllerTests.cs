using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class SurpriseControllerTests
    {
        private Mock<IEntityRepository<LibraryBook>> bookRepositoryMock;
        private Mock<IEntityRepository<Person>> peopleRepositoryMock;
        private SurpriseController controller;
        private LibraryBook testBook;
        private Person testPerson;


        [SetUp]
        public void SetUp()
        {
            bookRepositoryMock = new Mock<IEntityRepository<LibraryBook>>();
            peopleRepositoryMock = new Mock<IEntityRepository<Person>>();
            controller = new SurpriseController(peopleRepositoryMock.Object, bookRepositoryMock.Object);
            testBook = new LibraryBook();
            testPerson = new Person();
        }


        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_Books_In_Database()
        {
            bookRepositoryMock.Setup(repo => repo.Items).Returns((new LibraryBook[] { }).AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns((new Person[] { }).AsQueryable());

            ActionResult result = controller.Index();

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }

        [Test]
        public void Index_Will_Redirect_To_Books_Controller_For_Surprise_Books()
        {
            peopleRepositoryMock.Setup(repo => repo.Items).Returns((new [] { testPerson }).AsQueryable());
            bookRepositoryMock.Setup(repo => repo.Items).Returns((new [] { testBook }).AsQueryable());

            bookRepositoryMock.Setup(repo => repo.GetRandomItem()).Returns(testBook);
            
            var randomEntityPickerMock = new Mock<IRandomEntityPicker>();
            randomEntityPickerMock.Setup(picker => picker.getRandomEntityType()).Returns(typeof(LibraryBook));

            var result = (RedirectToRouteResult)controller.Index(randomEntityPickerMock.Object);

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
        }
    }
}
