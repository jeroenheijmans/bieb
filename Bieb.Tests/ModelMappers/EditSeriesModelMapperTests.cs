using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Models;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    public class EditSeriesModelMapperTests
    {
        private EditSeriesModelMapper mapper;
        private IEntityRepository<Book> books;
        private Book frankenstein, dracula;


        [SetUp]
        public void SetUp()
        {
            books = new RepositoryMock<Book>();
            mapper = new EditSeriesModelMapper(books);
            frankenstein = new Book { Id = 1, Title = "Frankenstein" };
            dracula = new Book { Id = 2, Title = "Dracula" }; ;
        }


        [Test]
        public void Will_Have_Available_Books()
        {
            books.Add(frankenstein);
            books.Add(dracula);

            var model = mapper.ModelFromEntity(new Series());

            var availableIds = model.AvailableBooks.Select(book => book.Value);

            Assert.That(availableIds, Contains.Item(frankenstein.Id.ToString()));
            Assert.That(availableIds, Contains.Item(dracula.Id.ToString()));
        }


        [Test]
        public void Will_Sort_Available_Books()
        {
            books.Add(frankenstein);
            books.Add(dracula);

            var model = mapper.ModelFromEntity(new Series());

            var availableIds = model.AvailableBooks.Select(book => book.Value);

            Assert.That(availableIds.Skip(0).First(), Is.EqualTo(dracula.Id.ToString()));
            Assert.That(availableIds.Skip(1).First(), Is.EqualTo(frankenstein.Id.ToString()));
        }
    }
}
