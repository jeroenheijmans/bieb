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
            var myBook = new Book();
            
            var story1 = new Story();
            var story2 = new Story();

            var tag1 = new Tag { Name = "Science Fiction" };
            var tag2 = new Tag { Name = "Fantasy" };
            var unusedTag = new Tag { Name = "Educational" };

            myBook.Stories.Add(1, story1);
            myBook.Stories.Add(2, story2);
            story1.Tags.Add(tag1);
            story1.Tags.Add(tag2);
            story2.Tags.Add(tag1);

            Assert.That(myBook.AllTags.Count(), Is.EqualTo(2));
            Assert.That(myBook.AllTags.ToList(), Has.Member(tag1));
            Assert.That(myBook.AllTags.ToList(), Has.Member(tag2));
            Assert.That(myBook.AllTags.ToList(), Has.No.Member(unusedTag));
        }


        [Test]
        public void Has_Single_Author_For_Novel()
        {
            var novel = new Book();

            var story = new Story();
            novel.Stories.Add(1, story);

            var person = new Person();
            story.Authors.Add(person);
            
            Assert.That(novel.AllAuthors.Count(), Is.EqualTo(1));
            Assert.That(novel.AllAuthors.First(), Is.EqualTo(person));
        }


        [Test]
        public void Contains_Authors_From_Stories()
        {
            var myBook = new Book();

            var story1 = new Story();
            var story2 = new Story();

            var asimov = new Person();
            var tolkien = new Person();

            story1.Authors.Add(asimov);
            story2.Authors.Add(tolkien);
            myBook.Stories.Add(1, story1);
            myBook.Stories.Add(2, story2);

            Assert.That(myBook.AllAuthors.Count(), Is.EqualTo(2));
            Assert.That(myBook.AllAuthors.ToList(), Has.Member(asimov));
            Assert.That(myBook.AllAuthors.ToList(), Has.Member(tolkien));
        }


        [Test]
        public void Contains_CoAuthors_From_Stories()
        {
            var myBook = new Book();

            var story1 = new Story();

            var tolkien = new Person();
            var sonOfTolkien = new Person();

            story1.Authors.Add(tolkien);
            story1.Authors.Add(sonOfTolkien);
            myBook.Stories.Add(1, story1);

            Assert.That(myBook.AllAuthors.Count(), Is.EqualTo(2));
            Assert.That(myBook.AllAuthors.ToList(), Has.Member(tolkien));
            Assert.That(myBook.AllAuthors.ToList(), Has.Member(sonOfTolkien));
        }


        [Test]
        public void Contains_Translators_From_Stories()
        {
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

            Assert.That(myBook.AllTranslators.Count(), Is.EqualTo(2));
            Assert.That(myBook.AllTranslators.ToList(), Has.Member(pjotr));
            Assert.That(myBook.AllTranslators.ToList(), Has.Member(michelle));
        }

        
        [Test]
        public void Is_Novel_If_Book_Has_Zero_Stories()
        {
            var myBook = new Book();

            Assert.That(myBook.BookType, Is.EqualTo(BookType.Novel));
        }


        [Test]
        public void Is_Novel_If_Book_Has_One_Story()
        {
            var myBook = new Book();
            var story = new Story();
            myBook.Stories.Add(1, story);

            Assert.That(myBook.BookType, Is.EqualTo(BookType.Novel));
        }


        [Test]
        public void Can_Discern_Different_BookTypes()
        {
            var asimov = new Person { Surname = "Asimov" };
            var clarke = new Person { Surname = "Clarke" };

            Story asimovStory1 = new Story(), asimovStory2 = new Story(), clarkeStory = new Story();

            asimovStory1.Authors.Add(asimov);
            asimovStory2.Authors.Add(asimov);
            clarkeStory.Authors.Add(clarke);

            var novel = new Book { Title = "How Novel!" };
            var collection = new Book { Stories = new Dictionary<int, Story>() };
            var anthology = new Book { Stories = new Dictionary<int, Story>() };

            collection.Stories.Add(0, asimovStory1);
            collection.Stories.Add(1, asimovStory2);

            anthology.Stories.Add(0, asimovStory1);
            anthology.Stories.Add(1, asimovStory2);
            anthology.Stories.Add(2, clarkeStory);

            Assert.That(novel.BookType, Is.EqualTo(BookType.Novel));
            Assert.That(collection.BookType, Is.EqualTo(BookType.Collection));
            Assert.That(anthology.BookType, Is.EqualTo(BookType.Anthology));
        }


        [Test]
        public void Is_Tagged_Based_On_All_Stories()
        {
            Tag cool = new Tag(), hot = new Tag(), old = new Tag(), sweet = new Tag();
            Story story1 = new Story(), story2 = new Story(), story3 = new Story();

            story1.Tags.Add(cool);
            story1.Tags.Add(hot);
            story2.Tags.Add(cool);
            story2.Tags.Add(old);
            story3.Tags.Add(sweet);

            var theHobbit = new Book();
            theHobbit.Stories.Add(0, story1);
            theHobbit.Stories.Add(1, story2);
            theHobbit.Stories.Add(2, story3);            

            Assert.That(theHobbit.AllTags.Contains(cool));
            Assert.That(theHobbit.AllTags.Contains(hot));
            Assert.That(theHobbit.AllTags.Contains(old));
            Assert.That(theHobbit.AllTags.Contains(sweet));
            Assert.That(theHobbit.AllTags.Count(), Is.EqualTo(4));
        }


        [Test]
        public void Can_Set_And_Retrieve_Series()
        {
            var book = new Book();
            var series = new Series();

            book.Series = series;

            Assert.That(book.Series, Is.EqualTo(series));
        }


        [Test]
        public void ToString_Will_Return_Name()
        {
            var entity = new Book() { Title = "Xyz" };
            Assert.That(entity.ToString(), Is.EqualTo(entity.Title));
        }


        [Test]
        public void ToString_Can_Handle_Null_Name()
        {
            var entity = new Book() { Title = null };
            Assert.That(entity.ToString(), Is.Not.Null.Or.Empty);
        }


        [Test]
        public void Can_Add_Reference_Books_To_New_Book()
        {
            var book = new Book();
            Assert.DoesNotThrow(() => book.ReferencedByBooks.Add(book));
        }
    }
}
