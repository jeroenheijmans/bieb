using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.Cascades
{
    [TestFixture]
    public class BookCascadeTests : DatabaseIntegrationTest
    {
        [Test]
        public void Save_Book_Will_Cascade_To_Stories()
        {
            var book = new Book("March of the machines");
            var story = new Story();
            book.AddStory(story);

            Session.Save(book);
            
            Assert.DoesNotThrow(Session.Flush);
        }
    }
}
