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
        private Mock<IEntityRepository<Book>> repositoryMock;
        private BooksController controller;


        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IEntityRepository<Book>>();
            controller = new BooksController(repositoryMock.Object);
        }


        [Test]
        public void Index_Will_Have_Default_TitleSort_Sorting()
        {
            var libraryBook1 = new LibraryBook { Title = "Zoltan the Great" };
            var libraryBook2 = new LibraryBook { Title = "Middle-man" };
            var libraryBook3 = new LibraryBook { Title = "Alpha came before Omega" };

            repositoryMock.Setup(repo => repo.Items).Returns((new[] { libraryBook1, libraryBook2, libraryBook3 }).AsQueryable());

            ActionResult result = controller.Index();

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<LibraryBook>>());

            var libraryBookList = (PagedList<LibraryBook>)vresult.Model;

            Assert.That(libraryBookList[0], Is.EqualTo(libraryBook3));
            Assert.That(libraryBookList[1], Is.EqualTo(libraryBook2));
            Assert.That(libraryBookList[2], Is.EqualTo(libraryBook1));
        }


        [Test]
        public void Index_Will_Only_Show_Library_Books()
        {
            repositoryMock.Setup(repo => repo.Items).Returns((new Book[] { new LibraryBook(), new ReferenceBook() }).AsQueryable());

            var result = (ViewResult) controller.Index();
            var books = result.Model as IEnumerable<Book>;

            Assert.That(books, Is.Not.Null);
            Assert.That(books.Count(), Is.EqualTo(1));
        }
    }
}
