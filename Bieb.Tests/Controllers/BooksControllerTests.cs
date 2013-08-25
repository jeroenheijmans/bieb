using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using NUnit.Framework;
using PagedList;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private IEntityRepository<Book> repository;
        private BooksController controller;


        [SetUp]
        public void SetUp()
        {
            repository = new RepositoryMock<Book>();
            controller = new BooksController(repository, null);
        }


        [Test]
        public void Index_Will_Have_Default_TitleSort_Sorting()
        {
            var libraryBook1 = new LibraryBook { Title = "Zoltan the Great" };
            var libraryBook2 = new LibraryBook { Title = "Middle-man" };
            var libraryBook3 = new LibraryBook { Title = "Alpha came before Omega" };
            
            repository.Add(libraryBook1);
            repository.Add(libraryBook2);
            repository.Add(libraryBook3);

            ActionResult result = controller.Index();

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<Book>>());

            var libraryBookList = (PagedList<Book>)vresult.Model;

            Assert.That(libraryBookList[0], Is.EqualTo(libraryBook3));
            Assert.That(libraryBookList[1], Is.EqualTo(libraryBook2));
            Assert.That(libraryBookList[2], Is.EqualTo(libraryBook1));
        }


        [Test]
        public void Index_Will_Only_Show_Library_Books()
        {
            repository.Add(new LibraryBook());
            repository.Add(new ReferenceBook());

            var result = (ViewResult) controller.Index();
            var books = result.Model as IEnumerable<Book>;

            Assert.That(books, Is.Not.Null);
            Assert.That(books.Count(), Is.EqualTo(1));
        }
    }
}
