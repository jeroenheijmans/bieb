using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Bieb.Domain.Entities;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void Contains_Tags_From_Stories()
        {
            // Arrange
            var myBook = new Book();
            
            var story1 = new Story();
            var story2 = new Story();

            var tag1 = new Tag() { Name = "Science Fiction" };
            var tag2 = new Tag() { Name = "Fantasy" };
            var unusedTag = new Tag() { Name = "Educational" };

            myBook.Stories.Add(1, story1);
            myBook.Stories.Add(2, story2);
            story1.Tags.Add(tag1);
            story1.Tags.Add(tag2);
            story2.Tags.Add(tag1);

            // Act
            // ...

            // Assert
            Assert.That(myBook.Tags.Count(), Is.EqualTo(2));
            Assert.That(myBook.Tags.ToList(), Has.Member(tag1));
            Assert.That(myBook.Tags.ToList(), Has.Member(tag2));
            Assert.That(myBook.Tags.ToList(), Has.No.Member(unusedTag));
        }

        [Test]
        public void Has_Single_Author_For_Novel()
        {
            // Arrange
            var novel = new Book();

            var story = new Story();
            novel.Stories.Add(1, story);

            var person = new Person();
            story.Authors.Add(person);
            
            // Act
            // ...

            // Assert
            Assert.That(novel.Authors.Count(), Is.EqualTo(1));
            Assert.That(novel.Authors.First(), Is.EqualTo(person));
        }

        [Test]
        public void Contains_Authors_From_Stories()
        {
            // Arrange
            var myBook = new Book();

            var story1 = new Story();
            var story2 = new Story();

            var asimov = new Person();
            var tolkien = new Person();

            story1.Authors.Add(asimov);
            story2.Authors.Add(tolkien);
            myBook.Stories.Add(1, story1);
            myBook.Stories.Add(2, story2);

            // Act
            // ...

            // Assert
            Assert.That(myBook.Authors.Count(), Is.EqualTo(2));
            Assert.That(myBook.Authors.ToList(), Has.Member(asimov));
            Assert.That(myBook.Authors.ToList(), Has.Member(tolkien));
        }

        [Test]
        public void Contains_CoAuthors_From_Stories()
        {
            // Arrange
            var myBook = new Book();

            var story1 = new Story();

            var tolkien = new Person();
            var sonOfTolkien = new Person();

            story1.Authors.Add(tolkien);
            story1.Authors.Add(sonOfTolkien);
            myBook.Stories.Add(1, story1);

            // Act
            // ...

            // Assert
            Assert.That(myBook.Authors.Count(), Is.EqualTo(2));
            Assert.That(myBook.Authors.ToList(), Has.Member(tolkien));
            Assert.That(myBook.Authors.ToList(), Has.Member(sonOfTolkien));
        }

        [Test]
        public void Contains_Translators_From_Stories()
        {
            // Arrange
            var myBook = new Book();

            var story1 = new Story();
            var story2 = new Story();

            var pjotr = new Person();
            var michelle = new Person();

            story1.Translators.Add(pjotr);
            story2.Translators.Add(pjotr);
            story2.Translators.Add(michelle);
            myBook.Stories.Add(1, story1);
            myBook.Stories.Add(2, story2);

            // Act
            // ...

            // Assert
            Assert.That(myBook.Translators.Count(), Is.EqualTo(2));
            Assert.That(myBook.Translators.ToList(), Has.Member(pjotr));
            Assert.That(myBook.Translators.ToList(), Has.Member(michelle));
        }
        
        [Test]
        public void Is_Novel_If_Book_Has_Zero_Stories()
        {
            // Arrange
            var myBook = new Book();

            // Act
            // ...

            // Assert
            Assert.That(myBook.BookType, Is.EqualTo(BookType.Novel));
        }

        [Test]
        public void Is_Novel_If_Book_Has_One_Story()
        {
            // Arrange
            var myBook = new Book();
            var story = new Story();
            myBook.Stories.Add(1, story);


            // Act
            // ...

            // Assert
            Assert.That(myBook.BookType, Is.EqualTo(BookType.Novel));
        }

        [Test]
        public void Can_Discern_Different_BookTypes()
        {
            // Arrange
            var asimov = new Person() { Surname = "Asimov" };
            var clarke = new Person() { Surname = "Clarke" };

            var asimovStory1 = new Story() { Authors = new Person[] { asimov } };
            var asimovStory2 = new Story() { Authors = new Person[] { asimov } };
            var clarkeStory = new Story() { Authors = new Person[] { clarke } };

            var novel = new Book() { Title = "How Novel!" };
            var collection = new Book() { Stories = new Dictionary<int, Story>() };
            var anthology = new Book() { Stories = new Dictionary<int, Story>() };

            collection.Stories.Add(0, asimovStory1);
            collection.Stories.Add(1, asimovStory2);

            anthology.Stories.Add(0, asimovStory1);
            anthology.Stories.Add(1, asimovStory2);
            anthology.Stories.Add(2, clarkeStory);

            // Act
            // ...

            // Assert
            Assert.That(novel.BookType, Is.EqualTo(BookType.Novel));
            Assert.That(collection.BookType, Is.EqualTo(BookType.Collection));
            Assert.That(anthology.BookType, Is.EqualTo(BookType.Anthology));
        }

        [Test]
        public void Is_Tagged_Based_On_All_Stories()
        {
            // Arrang
            Tag cool = new Tag(), hot = new Tag(), old = new Tag(), sweet = new Tag();

            var story1 = new Story() { Tags = new Tag[] { cool, hot } };
            var story2 = new Story() { Tags = new Tag[] { cool, old } };
            var story3 = new Story() { Tags = new Tag[] { sweet } };

            var theHobbit = new Book();
            theHobbit.Stories.Add(0, story1);
            theHobbit.Stories.Add(1, story2);
            theHobbit.Stories.Add(2, story3);            

            // Act 
            // ...

            // Assert
            Assert.That(theHobbit.Tags.Contains(cool));
            Assert.That(theHobbit.Tags.Contains(hot));
            Assert.That(theHobbit.Tags.Contains(old));
            Assert.That(theHobbit.Tags.Contains(sweet));
            Assert.That(theHobbit.Tags.Count(), Is.EqualTo(4));
        }
    }
}
