using System;
using System.Linq;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class PersonTests
    {
        private const string Title = "Dr. Mr.";
        private const string FirstName = "John J.";
        private const string Prefix = "of the";
        private const string Surname = "Hard-fasters";


        [Test]
        public void Can_Create_FullName_With_All_Values()
        {
            var john = new Person
            {
                Title = Title,
                FirstName = FirstName,
                Prefix = Prefix,
                Surname = Surname
            };

            const string expectedFullName = Title + " " + FirstName + " " + Prefix + " " + Surname;

            Assert.AreEqual(expectedFullName, john.FullName);
            Assert.AreEqual(Title, john.Title);
            Assert.AreEqual(FirstName, john.FirstName);
            Assert.AreEqual(Prefix, john.Prefix);
            Assert.AreEqual(Surname, john.Surname);
        }


        [Test]
        public void Can_Create_Simple_Alias()
        {
            var john = new Person
            {
                FirstName = FirstName,
                Surname = Surname
            };

            const string expectedFullName = FirstName + " " + Surname;

            Assert.AreEqual(expectedFullName, john.FullName);
            Assert.IsNull(john.Title);
            Assert.AreEqual(FirstName, john.FirstName);
            Assert.IsNull(john.Prefix);
            Assert.AreEqual(Surname, john.Surname);
        }


        [Test]
        public void Has_Tags_From_All_Related_Stories()
        {
            Tag cool = new Tag(), hot = new Tag(), old = new Tag(), sweet = new Tag();

            var story1 = new Story { Tags = new[] { cool, hot } };
            var story2 = new Story { Tags = new[] { cool, old } };
            var story3 = new Story { Tags = new[] { sweet } };

            var someBook = new Book();
            someBook.Stories.Add(1, story3);

            var tolkien = new Person();

            tolkien.AuthoredStories.Add(story1);
            tolkien.TranslatedStories.Add(story2);
            tolkien.EditedBooks.Add(someBook);

            Assert.That(tolkien.Tags.Contains(cool));
            Assert.That(tolkien.Tags.Contains(hot));
            Assert.That(tolkien.Tags.Contains(old));
            Assert.That(tolkien.Tags.Contains(sweet));
            Assert.That(tolkien.Tags.Count(), Is.EqualTo(4));
        }


        [Test]
        public void Gets_Roles_From_Stories_And_Books()
        {
            var sybrenPolet = new Person { FirstName = "Sybren", Surname = "Polet" };
            var story = new Story();
            var book = new Book();

            sybrenPolet.AuthoredStories.Add(story);
            sybrenPolet.TranslatedStories.Add(story);
            sybrenPolet.EditedBooks.Add(book);

            Assert.That(sybrenPolet.Roles.Contains(Role.Author));
            Assert.That(sybrenPolet.Roles.Contains(Role.Editor));
            Assert.That(sybrenPolet.Roles.Contains(Role.Translator));
            Assert.That(sybrenPolet.Roles.Count(), Is.EqualTo(3));
        }


        [Test]
        public void Can_Distinguish_Between_Translated_ShortStories_And_Novels()
        {
            var asimov = new Person { FirstName = "Isaac", Surname = "Asimov" };

            var shortStory1 = new Story();
            var shortStory2 = new Story();
            var novelStory = new Story();

            var collection = new Book();
            collection.Stories.Add(0, shortStory1);
            collection.Stories.Add(1, shortStory2);
            shortStory1.Book = collection;
            shortStory2.Book = collection;

            var novel = new Book();
            novel.Stories.Add(0, novelStory);
            novelStory.Book = novel;

            asimov.TranslatedStories.Add(shortStory1);
            asimov.TranslatedStories.Add(shortStory2);
            asimov.TranslatedStories.Add(novelStory);

            Assert.That(asimov.TranslatedStories.Count, Is.EqualTo(3));
            Assert.That(asimov.TranslatedNovels.Count(), Is.EqualTo(1));
            Assert.That(asimov.TranslatedNovels.ToList()[0], Is.EqualTo(novel));
            Assert.That(asimov.TranslatedShortStories.Count(), Is.EqualTo(2));
            Assert.That(asimov.TranslatedShortStories.ToList()[0], Is.EqualTo(shortStory1));
            Assert.That(asimov.TranslatedShortStories.ToList()[1], Is.EqualTo(shortStory2));
        }


        [Test]
        public void Can_Distinguish_Between_Authored_ShortStories_And_Novels()
        {
            var asimov = new Person { FirstName = "Isaac", Surname = "Asimov" };

            var shortStory1 = new Story();
            var shortStory2 = new Story();
            var novelStory = new Story();

            var collection = new Book();
            collection.Stories.Add(0, shortStory1);
            collection.Stories.Add(1, shortStory2);
            shortStory1.Book = collection;
            shortStory2.Book = collection;

            var novel = new Book();
            novel.Stories.Add(0, novelStory);
            novelStory.Book = novel;

            asimov.AuthoredStories.Add(shortStory1);
            asimov.AuthoredStories.Add(shortStory2);
            asimov.AuthoredStories.Add(novelStory);

            Assert.That(asimov.AuthoredStories.Count, Is.EqualTo(3));
            Assert.That(asimov.AuthoredNovels.Count(), Is.EqualTo(1));
            Assert.That(asimov.AuthoredNovels.ToList()[0], Is.EqualTo(novel));
            Assert.That(asimov.AuthoredShortStories.Count(), Is.EqualTo(2));
            Assert.That(asimov.AuthoredShortStories.ToList()[0], Is.EqualTo(shortStory1));
            Assert.That(asimov.AuthoredShortStories.ToList()[1], Is.EqualTo(shortStory2));
        }


        [Test]
        public void Equal_From_And_Until_DateOfBirth_Is_Certain_Date()
        {
            var someDate = DateTime.Now;
            var person = new Person { DateOfBirthFrom = someDate, DateOfBirthUntil = someDate };

            Assert.That(person.DateOfBirth.IsCertain);
        }


        [Test]
        public void Unequal_From_And_Until_DateOfBirth_Is_Not_Certain_Date()
        {
            var person = new Person { DateOfBirthFrom = new DateTime(1900,1,1), DateOfBirthUntil = new DateTime(2000,2,2) };

            Assert.That(person.DateOfBirth.IsCertain, Is.Not.True);
        }


        [Test]
        public void Setting_Uncertain_DateOfBirth_Will_Persist_In_From_And_Until_Dates()
        {
            var fromDateTime = new DateTime(1950, 1, 1);
            var untilDateTime = new DateTime(1950, 1, 31);
            var person = new Person();

            person.DateOfBirth = new UncertainDate(fromDateTime, untilDateTime);

            Assert.That(person.DateOfBirthFrom, Is.EqualTo(fromDateTime));
            Assert.That(person.DateOfBirthUntil, Is.EqualTo(untilDateTime));
        }


        [Test]
        public void Can_Generate_Alphabetical_Fullname_With_All_Parts()
        {
            var person = new Person
            {
                Title = "Sir",
                FirstName = "Arthur",
                Prefix = "von",
                Surname = "Munchhausen"
            };

            Assert.That(person.FullNameAlphabetical, Is.EqualTo("Munchhausen, Sir Arthur von"));
        }

        [Test]
        public void Can_Generate_Alphabetical_Fullname_For_Just_Surname()
        {
            var person = new Person
            {
                Surname = "Johnsson"
            };

            Assert.That(person.FullNameAlphabetical, Is.EqualTo("Johnsson"));
        }

        [Test]
        public void Can_Generate_Alphabetical_Fullname_For_First_Prefix_Surname()
        {
            var person = new Person
            {
                FirstName = "Karl",
                Prefix = "des",
                Surname = "Warnstein"
            };

            Assert.That(person.FullNameAlphabetical, Is.EqualTo("Warnstein, Karl des"));
        }

        [Test]
        public void Can_Generate_Alphabetical_Fullname_For_First_Surname()
        {
            var person = new Person
            {
                FirstName = "Isaac",
                Surname = "Asimov"
            };

            Assert.That(person.FullNameAlphabetical, Is.EqualTo("Asimov, Isaac"));
        }
    }
}
