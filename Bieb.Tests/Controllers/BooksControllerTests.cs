using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BooksControllerTests
    {
        private Mock<IEntityRepository<LibraryBook>> repositoryMock;


        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IEntityRepository<LibraryBook>>();
        }


        [Test]
        public void Index_Will_Have_Default_TitleSort_Sorting()
        {
            // Arrange
            var libraryBook1 = new LibraryBook { Title = "Zoltan the Great" };
            var libraryBook2 = new LibraryBook { Title = "Middle-man" };
            var libraryBook3 = new LibraryBook { Title = "Alpha came before Omega" };
            repositoryMock.Setup(repo => repo.Items).Returns((new[] { libraryBook1, libraryBook2, libraryBook3 }).AsQueryable());
            var controller = new BooksController(repositoryMock.Object);

            // Act & Assert
            ActionResult result = controller.Index();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<LibraryBook>>());

            var libraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(libraryBookList[0], Is.EqualTo(libraryBook3));
            Assert.That(libraryBookList[1], Is.EqualTo(libraryBook2));
            Assert.That(libraryBookList[2], Is.EqualTo(libraryBook1));
        }
    }
}
