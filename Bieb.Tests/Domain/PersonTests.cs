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

            tolkien.AddAuthoredStory(story1);
            tolkien.AddTranslatedStory(story2);
            tolkien.AddEditedBook(someBook);

            Assert.That(tolkien.Tags.Contains(cool));
            Assert.That(tolkien.Tags.Contains(hot));
            Assert.That(tolkien.Tags.Contains(old));
            Assert.That(tolkien.Tags.Contains(sweet));
            Assert.That(tolkien.Tags.Count(), Is.EqualTo(4));
        }


        [Test]
        public void Has_Tags_From_Authored_Books()
        {
            var cool = new Tag("Cool");
            var person = new Person();
            var book = new Book();
            book.Tags.Add(cool);
            person.AddAuthoredBook(book);

            Assert.That(person.Tags.FirstOrDefault(), Is.EqualTo(cool));
        }


        [Test]
        public void Can_Derive_Author_Role_From_AuthoredBooks()
        {
            var person = new Person();
            person.AddAuthoredBook(new Book());

            Assert.That(person.Roles.Contains(Role.Author));
        }


        [Test]
        public void Can_Derive_Author_Role_From_AuthoredStories()
        {
            var person = new Person();
            person.AddAuthoredStory(new Story());

            Assert.That(person.Roles.Contains(Role.Author));
        }


        [Test]
        public void Can_Derive_Translator_Role_From_TranslatedStories()
        {
            var person = new Person();
            person.AddTranslatedStory(new Story());

            Assert.That(person.Roles.Contains(Role.Translator));
        }


        [Test]
        public void Can_Derive_Translator_Role_From_TranslatedBooks()
        {
            var person = new Person();
            person.AddTranslatedBook(new Book());

            Assert.That(person.Roles.Contains(Role.Translator));
        }


        [Test]
        public void Can_Derive_Editor_Role_From_EditedBooks()
        {
            var person = new Person();
            person.AddEditedBook(new Book());

            Assert.That(person.Roles.Contains(Role.Editor));
        }


        [Test]
        public void Fresh_Person_Will_Have_No_Roles()
        {
            var person = new Person();
            Assert.That(person.Roles, Is.Empty);
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
