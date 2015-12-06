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
        public void Save_Book_With_Story_Will_Not_Throw_Exception()
        {
            var book = new Book("March of the machines");
            var story = new Story();
            book.AddStory(story);

            Session.Save(book);
            
            Assert.DoesNotThrow(Session.Flush);
        }

        [Test]
        public void Delete_Book_With_Stories_Will_Not_Throw_Exception()
        {
            var book = new Book("Jupiter vs Pluto");
            var story = new Story();
            book.AddStory(story);

            Session.Save(book);
            Session.Flush();
            Session.Delete(book);

            Assert.DoesNotThrow(Session.Flush);
        }


        [Test]
        public void Can_Save_Book()
        {
            var book = new Book
                           {
                               Title = "The Hobbit",
                               Subtitle = "and other stories",
                               Isbn = "1234567890"
                           };

            Session.Save(book);
            Session.Flush();
            Session.Refresh(book);

            Assert.That(book.Title, Is.EqualTo("The Hobbit"));
            Assert.That(book.Subtitle, Is.EqualTo("and other stories"));
            Assert.That(book.Isbn, Is.EqualTo("1234567890"));
        }


        [Test]
        public void Save_Will_Cascade_To_BookStories()
        {
            var book = new Book("Collection X");
            var story1 = new Story("Short story 1");
            book.AddStory(story1);
            Session.Save(book);
            Session.Flush();
            Session.Refresh(book);
            Assert.That(story1.Id, Is.Not.EqualTo(0));
        }

        
        [Test]
        public void Save_Will_Cascade_To_BookAuthors()
        {
            var person = new Person {Surname = "Asimov"};
            var book = new Book {Title = "Robot Dreams"};
            book.AddAuthor(person);
            Session.Save(person);
            Session.Save(book);
            Session.Flush();
            Session.Refresh(book);
            Assert.That(book.Authors.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Delete_Will_Cascade_To_BookAuthors()
        {
            var person = new Person { Surname = "Asimov" };
            var book = new Book { Title = "Robot Dreams" };
            book.AddAuthor(person);
            Session.Save(person);
            Session.Save(book);
            Session.Delete(book);
            Session.Flush();
        }


        [Test]
        public void Delete_Will_Cascade_To_BookEditors()
        {
            var person = new Person { Surname = "Edi Tor" };
            var book = new Book { Title = "Robot Dreams" };
            book.AddEditor(person);
            Session.Save(person);
            Session.Save(book);
            Session.Delete(book);
            Session.Flush();
        }


        [Test]
        public void Delete_Will_Cascade_To_BookTranslators()
        {
            var person = new Person { Surname = "Trans la Tore" };
            var book = new Book { Title = "Robot Dreams" };
            book.AddTranslator(person);
            Session.Save(person);
            Session.Save(book);
            Session.Delete(book);
            Session.Flush();
        }
    }
}
