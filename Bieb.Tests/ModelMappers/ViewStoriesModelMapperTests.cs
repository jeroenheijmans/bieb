using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewStoriesModelMapperTests
    {
        private IViewEntityModelMapper<Story, ViewStoryModel> mapper;
        private Mock<IIso639LanguageDisplayer> iso639LanguageDisplayerMock;

        [SetUp]
        public void SetUp()
        {
            iso639LanguageDisplayerMock = new Mock<IIso639LanguageDisplayer>();
            mapper = new ViewStoryModelMapper(iso639LanguageDisplayerMock.Object);
        }

        [Test]
        public void Will_Ask_For_Language_Localization()
        {
            mapper.ModelFromEntity(new Story {Iso639LanguageId = "nl"});
            iso639LanguageDisplayerMock.Verify(i => i.GetLocalizedIso639LanguageResource("nl"));
        }


        [Test]
        public void Can_Map_Authors()
        {
            var story = new Story();
            var person = new Person();
            story.AddAuthor(person);
            var result = mapper.ModelFromEntity(story);
            Assert.That(result.Authors.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Map_Translators()
        {
            var story = new Story();
            story.AddTranslator(new Person());
            var result = mapper.ModelFromEntity(story);
            Assert.That(result.Translators.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Map_Sibling_Stories()
        {
            var story = new Story { Id = 1 };
            var siblingstory = new Story { Id = 2 };
            var book = new Book();
            book.AddStory(story);
            book.AddStory(siblingstory);
            var result = mapper.ModelFromEntity(story);
            Assert.That(result.SiblingStories.Single().Id, Is.EqualTo(siblingstory.Id));
        }
    }
}
