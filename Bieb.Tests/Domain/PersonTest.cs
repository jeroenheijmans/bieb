using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class PersonTest
    {
        private string title = "Dr. Mr.";
        private string firstName = "John J.";
        private string prefix = "of the";
        private string surname = "Hard-fasters";

        [Test]
        public void Can_Create_FullName_With_All_Values()
        {
            // Arrange
            Person john = new Person()
            {
                Title = this.title,
                FirstName = this.firstName,
                Prefix = this.prefix,
                Surname = this.surname
            };

            // Act 
            // ...

            // Assert
            string expectedFullName = title + " " + firstName + " " + prefix + " " + surname;

            Assert.AreEqual(expectedFullName, john.FullName);
            Assert.AreEqual(title, john.Title);
            Assert.AreEqual(firstName, john.FirstName);
            Assert.AreEqual(prefix, john.Prefix);
            Assert.AreEqual(surname, john.Surname);
        }

        [Test]
        public void Can_Create_Simple_Alias()
        {
            // Arrange
            Person john = new Person()
            {
                FirstName = this.firstName,
                Surname = this.surname
            };

            // Act 
            // ..

            // Assert
            string expectedFullName = firstName + " " + surname;

            Assert.AreEqual(expectedFullName, john.FullName);
            Assert.IsNull(john.Title);
            Assert.AreEqual(firstName, john.FirstName);
            Assert.IsNull(john.Prefix);
            Assert.AreEqual(surname, john.Surname);
        }

        [Test]
        public void Has_Tags_From_All_Related_Stories()
        {
            // Arrange
            Tag cool = new Tag(), hot = new Tag(), old = new Tag(), sweet = new Tag();

            Story story1 = new Story() { Tags = new Tag[] { cool, hot } };
            Story story2 = new Story() { Tags = new Tag[] { cool, old } };
            Story story3 = new Story() { Tags = new Tag[] { sweet } };

            Book someBook = new Book();
            someBook.Stories.Add(1, story3);

            Person tolkien = new Person();

            tolkien.AuthoredStories.Add(story1);
            tolkien.TranslatedStories.Add(story2);
            tolkien.EditedBooks.Add(someBook);

            // Act 
            // ..

            // Assert
            Assert.That(tolkien.Tags.Contains(cool));
            Assert.That(tolkien.Tags.Contains(hot));
            Assert.That(tolkien.Tags.Contains(old));
            Assert.That(tolkien.Tags.Contains(sweet));
            Assert.That(tolkien.Tags.Count(), Is.EqualTo(4));
        }

        [Test]
        public void Gets_Roles_From_Stories_And_Books()
        {
            // Arrange
            Person SybrenPolet = new Person() { FirstName = "Sybren", Surname = "Polet" };
            Story story = new Story();
            Book book = new Book();

            // Act
            SybrenPolet.AuthoredStories.Add(story);
            SybrenPolet.TranslatedStories.Add(story);
            SybrenPolet.EditedBooks.Add(book);

            // Assert
            Assert.That(SybrenPolet.Roles.Contains(Role.Author));
            Assert.That(SybrenPolet.Roles.Contains(Role.Editor));
            Assert.That(SybrenPolet.Roles.Contains(Role.Translator));
            Assert.That(SybrenPolet.Roles.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Can_Distinguish_Between_ShortStories_And_Novels()
        {
            // Arrange
            Person asimov = new Person() { FirstName = "Isaac", Surname = "Asimov" };

            Story shortStory1 = new Story(), shortStory2 = new Story();
            Story novelStory = new Story();

            Book collection = new Book();
            collection.Stories.Add(0, shortStory1);
            collection.Stories.Add(1, shortStory2);
            shortStory1.Book = collection;
            shortStory2.Book = collection;

            Book novel = new Book();
            novel.Stories.Add(0, novelStory);
            novelStory.Book = novel;

            asimov.AuthoredStories.Add(shortStory1);
            asimov.AuthoredStories.Add(shortStory2);
            asimov.AuthoredStories.Add(novelStory);

            // Act & Assert
            Assert.That(asimov.AuthoredStories.Count, Is.EqualTo(3));
            Assert.That(asimov.AuthoredNovels.Count(), Is.EqualTo(1));
            Assert.That(asimov.AuthoredNovels.ToList()[0], Is.EqualTo(novel));
            Assert.That(asimov.AuthoredShortStories.Count(), Is.EqualTo(2));
            Assert.That(asimov.AuthoredShortStories.ToList()[0], Is.EqualTo(shortStory1));
            Assert.That(asimov.AuthoredShortStories.ToList()[1], Is.EqualTo(shortStory2));
        }
    }
}
