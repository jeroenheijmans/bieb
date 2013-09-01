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
            var book1 = new Book { Title = "Zoltan the Great" };
            var book2 = new Book { Title = "Middle-man" };
            var book3 = new Book { Title = "Alpha came before Omega" };
            
            repository.Add(book1);
            repository.Add(book2);
            repository.Add(book3);

            ActionResult result = controller.Index();

            var vresult = (ViewResult)result;

            Assert.That(vresult.Model, Is.InstanceOf<PagedList<Book>>());

            var bookList = (PagedList<Book>)vresult.Model;

            Assert.That(bookList[0], Is.EqualTo(book3));
            Assert.That(bookList[1], Is.EqualTo(book2));
            Assert.That(bookList[2], Is.EqualTo(book1));
        }


        [Test]
        public void Index_Will_Not_Show_Reference_Only_Books()
        {
            repository.Add(new Book {LibraryStatus = LibraryStatus.InPosession});
            repository.Add(new Book {LibraryStatus = LibraryStatus.OnlyForReference});

            var result = (ViewResult) controller.Index();
            var books = (IEnumerable<Book>) result.Model;

            Assert.That(books.Count(), Is.EqualTo(1));
        }
    }
}
