using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Publishers;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewPublisherModelMapperTests
    {
        private IViewEntityModelMapper<Publisher, ViewPublisherModel> mapper;


        [SetUp]
        public void SetUp()
        {
            var iso639LanguageDisplayer = new Mock<IIso639LanguageDisplayer>().Object;
            var bookMapper = new ViewBookModelMapper(iso639LanguageDisplayer);
            var storyMapper = new ViewStoryModelMapper(iso639LanguageDisplayer);
            mapper = new ViewPublisherModelMapper(bookMapper, storyMapper);
        }


        [Test]
        public void Will_Have_Number_Of_Books_Equal_To_Entity_Books()
        {
            var publisher = new Publisher();
            publisher.Books.Add(new Book());
            var model = mapper.ModelFromEntity(publisher);
            Assert.That(model.MyBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Number_Of_ReferenceBooks_Equal_To_Entity_Books()
        {
            var publisher = new Publisher();
            publisher.Books.Add(new Book { LibraryStatus = LibraryStatus.OnlyForReference });
            var model = mapper.ModelFromEntity(publisher);
            Assert.That(model.ReferenceBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Number_Of_Stories_Equal_To_Entity_Stories()
        {
            var publisher = new Publisher();
            publisher.Stories.Add(new Story());
            var model = mapper.ModelFromEntity(publisher);
            Assert.That(model.Stories.Count(), Is.EqualTo(1));
        }
    }
}
