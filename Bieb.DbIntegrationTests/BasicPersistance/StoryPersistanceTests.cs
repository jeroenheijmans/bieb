using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public class StoryPersistanceTests : EntityPersistanceTests<Story>
    {
        protected override Story GetTypicalEntity()
        {
            return new Story
                       {
                           Title = "The Long Haul",
                           Subtitle = "and the road ahead",
                           Iso639LanguageId = "en",
                           Year = 2001
                       };
        }

        protected override void AssertEntityBasePropertiesAreEqual(Story actual, Story expected)
        {
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Subtitle, Is.EqualTo(expected.Subtitle));
            Assert.That(actual.Iso639LanguageId, Is.EqualTo(expected.Iso639LanguageId));
            Assert.That(actual.Year, Is.EqualTo(expected.Year));
        }
    }
}
