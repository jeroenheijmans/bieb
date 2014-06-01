using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Models;
using Bieb.Web.Models.Series;
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

            Assert.That(availableIds.First(), Is.EqualTo(dracula.Id.ToString()));
            Assert.That(availableIds.Second(), Is.EqualTo(frankenstein.Id.ToString()));
        }


        [Test]
        public void Will_Merge_Chosen_Books_Into_Domain_Object()
        {
            books.Add(frankenstein);
            books.Add(dracula);

            var series = new Series();
            var model = mapper.ModelFromEntity(series);

            model.BookIds = new int[] { frankenstein.Id, dracula.Id };

            mapper.MergeEntityWithModel(series, model);

            Assert.That(series.Books.Count() == 2);
        }


        [Test]
        public void Will_Throw_When_Provided_With_NonExisting_Book_Id()
        {
            var model = new EditSeriesModel{ BookIds = new[] { -42 } };

            var series = new Series();

            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(series, model));
        }


        [Test]
        public void Model_Will_Have_Current_BookIds_Preselected()
        {
            var series = new Series();
            series.Books.Add(1, dracula);
            series.Books.Add(2, frankenstein);

            var model = mapper.ModelFromEntity(series);

            Assert.That(model.BookIds[0], Is.EqualTo(dracula.Id));
            Assert.That(model.BookIds[1], Is.EqualTo(frankenstein.Id));
        }
    }
}
