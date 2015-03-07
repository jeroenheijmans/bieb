using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.Cascades
{
    [TestFixture]
    public class StoryCascadeTests : DatabaseIntegrationTest
    {
        [Test]
        public void Save_Will_Cascade_To_StoryAuthors()
        {
            var person = new Person {Surname = "Asimov"};
            var story = new Story();
            story.AddAuthor(person);
            Session.Save(person);
            Session.Save(story);
            Session.Flush();
            Session.Refresh(story);
            Assert.That(story.Authors, Is.Not.Empty);
        }
    }
}
