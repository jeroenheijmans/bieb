using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class StoryTests
    {
        [Test]
        public void Story_Without_Own_Language_Will_Use_Language_From_Book()
        {
            var book = new LibraryBook { IsbnLanguage = 90 };
            var story = new Story { Book = book };

            Assert.That(story.IsbnLanguage, Is.EqualTo(90));
        }


        [Test]
        public void Story_With_Own_Language_Will_Use_That_Language_Over_Books_Language()
        {
            var book = new LibraryBook { IsbnLanguage = 90 };
            var story = new Story { Book = book, IsbnLanguage = 1 };

            Assert.That(story.IsbnLanguage, Is.EqualTo(1));
        }
    }
}
