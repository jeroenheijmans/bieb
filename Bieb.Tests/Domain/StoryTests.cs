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
            var book = new Book { IsbnLanguage = 90 };
            var story = new Story { Book = book };

            Assert.That(story.IsbnLanguage, Is.EqualTo(90));
        }


        [Test]
        public void Story_With_Own_Language_Will_Use_That_Language_Over_Books_Language()
        {
            var book = new Book { IsbnLanguage = 90 };
            var story = new Story { Book = book, IsbnLanguage = 1 };

            Assert.That(story.IsbnLanguage, Is.EqualTo(1));
        }


        [Test]
        public void ToString_Will_Return_Name()
        {
            var entity = new Story() { Title = "Xyz" };
            Assert.That(entity.ToString(), Is.EqualTo(entity.Title));
        }


        [Test]
        public void ToString_Can_Handle_Null_Name()
        {
            var entity = new Story() { Title = null };
            Assert.That(entity.ToString(), Is.Not.Null.Or.Empty);
        }
    }
}
