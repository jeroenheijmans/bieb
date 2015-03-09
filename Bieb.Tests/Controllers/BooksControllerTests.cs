using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Moq;
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
            var viewModelMapper = new ViewBookModelMapper(new Mock<IIso639LanguageDisplayer>().Object);
            var editModelMapper = new Mock<IEditEntityModelMapper<Book, EditBookModel>>().Object;
            controller = new BooksController(repository, viewModelMapper, editModelMapper);
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

            var bookList = ((ViewResult)result).Model as StaticPagedList<ViewBookModel>;

            Assert.That(bookList, Is.Not.Null);
            Assert.That(bookList[0].Title, Is.EqualTo(book3.Title));
            Assert.That(bookList[1].Title, Is.EqualTo(book2.Title));
            Assert.That(bookList[2].Title, Is.EqualTo(book1.Title));
        }


        [Test]
        public void Index_Will_Not_Show_Reference_Only_Books()
        {
            repository.Add(new Book {LibraryStatus = LibraryStatus.InPosession});
            repository.Add(new Book {LibraryStatus = LibraryStatus.OnlyForReference});

            var result = (ViewResult) controller.Index();
            var books = (IEnumerable<ViewBookModel>) result.Model;

            Assert.That(books.Count(), Is.EqualTo(1));
        }
    }
}
