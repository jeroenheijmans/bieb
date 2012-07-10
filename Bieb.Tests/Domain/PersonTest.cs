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
    }
}
