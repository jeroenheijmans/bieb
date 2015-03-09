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
        public void Story_Can_Accept_Title_In_Constructor()
        {
            var story = new Story("Madness");
            Assert.That(story.Title, Is.EqualTo("Madness"));
        }


        [Test]
        public void Story_Without_Own_Language_Will_Use_Language_From_Book()
        {
            var book = new Book { Iso639LanguageId = "nl" };
            var story = new Story { Book = book };

            Assert.That(story.Iso639LanguageId, Is.EqualTo("nl"));
        }


        [Test]
        public void Story_With_Own_Language_Will_Use_That_Language_Over_Books_Language()
        {
            var book = new Book { Iso639LanguageId = "nl" };
            var story = new Story { Book = book, Iso639LanguageId = "en" };

            Assert.That(story.Iso639LanguageId, Is.EqualTo("en"));
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


        [Test]
        public void Can_Add_Author()
        {
            var story = new Story();
            var person = new Person();
            story.AddAuthor(person);
            Assert.That(story.Authors.Count(), Is.EqualTo(1));
            Assert.That(person.AuthoredStories.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Add_Translator()
        {
            var story = new Story();
            var person = new Person();
            story.AddTranslator(person);
            Assert.That(story.Translators.Count(), Is.EqualTo(1));
            Assert.That(person.TranslatedStories.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Remove_Author_Will_Remove_From_Story_Authors_Property()
        {
            var story = new Story();
            story.AddAuthor(new Person());
            story.RemoveAuthor(story.Authors.Single());
            Assert.That(story.Authors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Author_Will_Remove_From_Person_AuthoredStorys_Property()
        {
            var story = new Story();
            var person = new Person();
            story.AddAuthor(person);
            story.RemoveAuthor(person);
            Assert.That(person.AuthoredStories.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Translator_Will_Remove_From_Story_Translators_Property()
        {
            var story = new Story();
            story.AddTranslator(new Person());
            story.RemoveTranslator(story.Translators.Single());
            Assert.That(story.Translators.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Translator_Will_Remove_From_Person_TranslatedStorys_Property()
        {
            var story = new Story();
            var person = new Person();
            story.AddTranslator(person);
            story.RemoveTranslator(person);
            Assert.That(person.TranslatedStories.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearAuthors_Will_Make_Authors_Property_Empty()
        {
            var story = new Story();
            story.AddAuthor(new Person());
            story.ClearAuthors();
            Assert.That(story.Authors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearAuthors_Will_Remove_Story_From_Person_AuthoredStories()
        {
            var story = new Story();
            var person = new Person();
            story.AddAuthor(person);
            story.ClearAuthors();
            Assert.That(person.AuthoredStories.Count(), Is.EqualTo(0));
        }
    }
}
