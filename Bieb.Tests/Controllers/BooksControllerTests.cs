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
    }
}
