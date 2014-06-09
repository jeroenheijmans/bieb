using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Series;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class SeriesControllerTests
    {
        private SeriesController controller;
        private IEntityRepository<Series> repository;


        [SetUp]
        public void SetUp()
        {
            var bookMapper = new Mock<IViewEntityModelMapper<Book, ViewBookModel>>().Object;

            var viewMapper = new ViewSeriesModelMapper(bookMapper);
            var editMapperMock = new Mock<IEditEntityModelMapper<Series, EditSeriesModel>>();

            repository = new RepositoryMock<Series>();
            controller = new SeriesController(repository, viewMapper, editMapperMock.Object);
        }


        [Test]
        public void Index_Will_Sort_By_Title()
        {
            repository.Add(new Series {Id = 1, Title = "Omega"});
            repository.Add(new Series {Id = 2, Title = "Alfa"});

            var result = (ViewResult)controller.Index();
            var model = (IEnumerable<ViewSeriesModel>)result.Model;

            Assert.That(model.First().Id, Is.EqualTo(2));
            Assert.That(model.Second().Id, Is.EqualTo(1));
        }
    }
}
