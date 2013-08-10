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


        [SetUp]
        public void SetUp()
        {
            bookRepositoryMock = new Mock<IEntityRepository<LibraryBook>>();
            peopleRepositoryMock = new Mock<IEntityRepository<Person>>();
            controller = new SurpriseController(peopleRepositoryMock.Object, bookRepositoryMock.Object);
        }


        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_Books_In_Database()
        {
            // Arrange
            bookRepositoryMock.Setup(repo => repo.Items).Returns((new LibraryBook[] { }).AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns((new Person[] { }).AsQueryable());

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }

        [Test]
        public void Index_Will_Skip_Reference_Only_Books()
        {
            // Arrange
            peopleRepositoryMock.Setup(repo => repo.Items).Returns((new Person[] { }).AsQueryable());

            // TODO: add books, including several "reference books"
            // make sure these are reliable "skipped"

            bookRepositoryMock.Setup(repo => repo.Items).Returns((new LibraryBook[] { }).AsQueryable());
            
            var randomEntityPickerMock = new Mock<IRandomEntityPicker>();
            randomEntityPickerMock.Setup(picker => picker.getRandomEntityType()).Returns(typeof(LibraryBook));

            // Act
            ActionResult result = controller.Index(randomEntityPickerMock.Object);

            // Assert
            throw new InconclusiveException("Test not finished yet.");
        }
    }
}
