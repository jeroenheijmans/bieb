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
        private Mock<IIsbnLanguageDisplayer> isbnLanguageDisplayerMock;

        [SetUp]
        public void SetUp()
        {
            isbnLanguageDisplayerMock = new Mock<IIsbnLanguageDisplayer>();
            mapper = new ViewStoryModelMapper(isbnLanguageDisplayerMock.Object);
        }

        [Test]
        public void Will_Ask_For_Language_Localization()
        {
            mapper.ModelFromEntity(new Story{IsbnLanguage = 90});
            isbnLanguageDisplayerMock.Verify(i => i.GetLocalizedIsbnLanguageResource(90));
        }


        [Test]
        public void Can_Map_Authors()
        {
            var story = new Story();
            story.Authors.Add(new Person());
            var result = mapper.ModelFromEntity(story);
            Assert.That(result.Authors.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Map_Translators()
        {
            var story = new Story();
            story.Translators.Add(new Person());
            var result = mapper.ModelFromEntity(story);
            Assert.That(result.Translators.Count(), Is.EqualTo(1));
        }
    }
}
