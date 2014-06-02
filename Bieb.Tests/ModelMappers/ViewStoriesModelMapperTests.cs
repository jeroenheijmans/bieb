using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewStoriesModelMapperTests
    {
        [Test]
        public void Will_Ask_For_Language_Localization()
        {
            var isbnLanguageDisplayer = new Mock<IIsbnLanguageDisplayer>();
            var mapper = new ViewStoryModelMapper(isbnLanguageDisplayer.Object);
            mapper.ModelFromEntity(new Story{IsbnLanguage = 90});
            isbnLanguageDisplayer.Verify(i => i.GetLocalizedIsbnLanguageResource(90));
        }
    }
}
