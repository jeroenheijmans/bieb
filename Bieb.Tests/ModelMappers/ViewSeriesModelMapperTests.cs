using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Series;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewSeriesModelMapperTests
    {
        private IViewEntityModelMapper<Series, ViewSeriesModel> mapper;

        
        [SetUp]
        public void SetUp()
        {
            var iso639LanguageDisplayer = new Mock<IIso639LanguageDisplayer>().Object;
            var bookMapper = new ViewBookModelMapper(iso639LanguageDisplayer);
            mapper = new ViewSeriesModelMapper(bookMapper);
        }


        [Test]
        public void Will_Map_Correct_Number_Of_Books()
        {
            var series = new Series();
            series.Books.Add(1, new Book());
            var model = mapper.ModelFromEntity(series);
            Assert.That(model.Books.Count(), Is.EqualTo(1));
        }
    }
}
