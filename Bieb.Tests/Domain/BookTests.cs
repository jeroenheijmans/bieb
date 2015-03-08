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

            myBook.AddStory(story1);
            myBook.AddStory(story2);
            story1.AddTag(tag1);
            story1.AddTag(tag2);
            story2.AddTag(tag1);

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
            novel.AddStory(story);

            var person = new Person();
            story.AddAuthor(person);
            
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

            story1.AddAuthor(asimov);
            story2.AddAuthor(tolkien);

            myBook.AddStory(story1);
            myBook.AddStory(story2);

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

            story1.AddAuthor(tolkien);
            story1.AddAuthor(sonOfTolkien);
            myBook.AddStory(story1);

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

            story1.AddTranslator(pjotr);
            story2.AddTranslator(pjotr);
            story2.AddTranslator(michelle);
            myBook.AddStory(story1);
            myBook.AddStory(story2);

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
        public void Is_Collection_If_Book_Has_Stories()
        {
            var myBook = new Book();
            var story = new Story();
            myBook.AddStory(story);

            Assert.That(myBook.BookType, Is.EqualTo(BookType.Collection));
        }


        [Test]
        public void Can_Discern_Different_BookTypes()
        {
            var asimov = new Person { Surname = "Asimov" };
            var clarke = new Person { Surname = "Clarke" };

            Story asimovStory1 = new Story(), asimovStory2 = new Story(), clarkeStory = new Story();

            asimovStory1.AddAuthor(asimov);
            asimovStory2.AddAuthor(asimov);
            clarkeStory.AddAuthor(clarke);
            
            var novel = new Book { Title = "How Novel!" };

            var collection = new Book();
            collection.AddStory(asimovStory1);
            collection.AddStory(asimovStory2);

            var anthology = new Book();
            anthology.AddStory(asimovStory1);
            anthology.AddStory(clarkeStory);

            Assert.That(novel.BookType, Is.EqualTo(BookType.Novel));
            Assert.That(collection.BookType, Is.EqualTo(BookType.Collection));
            Assert.That(anthology.BookType, Is.EqualTo(BookType.Anthology));
        }


        [Test]
        public void Is_Tagged_Based_On_All_Stories()
        {
            Tag cool = new Tag(), hot = new Tag(), old = new Tag(), sweet = new Tag();
            Story story1 = new Story(), story2 = new Story(), story3 = new Story();

            story1.AddTag(cool);
            story1.AddTag(hot);
            story2.AddTag(cool);
            story2.AddTag(old);
            story3.AddTag(sweet);

            var theHobbit = new Book();
            theHobbit.AddStory(story1);
            theHobbit.AddStory(story2);
            theHobbit.AddStory(story3);            

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
        public void Can_Add_Reference_Book()
        {
            var book = new Book();
            Assert.DoesNotThrow(() => book.ReferenceBook = new Book());
        }


        [Test]
        public void Can_Determine_Position_In_Book_For_Stories_Automatically()
        {
            var book = new Book();
            Assert.DoesNotThrow(() => book.AddStory(new Story()));
            Assert.DoesNotThrow(() => book.AddStory(new Story()));
        }


        [Test]
        public void Can_Add_Author()
        {
            var book = new Book();
            var person = new Person();
            book.AddAuthor(person);
            Assert.That(book.Authors.Count(), Is.EqualTo(1));
            Assert.That(person.AuthoredBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Add_Translator()
        {
            var book = new Book();
            var person = new Person();
            book.AddTranslator(person);
            Assert.That(book.Translators.Count(), Is.EqualTo(1));
            Assert.That(person.TranslatedBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Add_Editor()
        {
            var book = new Book();
            var person = new Person();
            book.AddEditor(person);
            Assert.That(book.Editors.Count(), Is.EqualTo(1));
            Assert.That(person.EditedBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Remove_Author_Will_Remove_From_Book_Authors_Property()
        {
            var book = new Book();
            book.AddAuthor(new Person());
            book.RemoveAuthor(book.Authors.Single());
            Assert.That(book.Authors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Author_Will_Remove_From_Person_AuthoredBooks_Property()
        {
            var book = new Book();
            var person = new Person();
            book.AddAuthor(person);
            book.RemoveAuthor(person);
            Assert.That(person.AuthoredBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Translator_Will_Remove_From_Book_Translators_Property()
        {
            var book = new Book();
            book.AddTranslator(new Person());
            book.RemoveTranslator(book.Translators.Single());
            Assert.That(book.Translators.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Translator_Will_Remove_From_Person_TranslatedBooks_Property()
        {
            var book = new Book();
            var person = new Person();
            book.AddTranslator(person);
            book.RemoveTranslator(person);
            Assert.That(person.TranslatedBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Editor_Will_Remove_From_Book_Editors_Property()
        {
            var book = new Book();
            book.AddEditor(new Person());
            book.RemoveEditor(book.Editors.Single());
            Assert.That(book.Editors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Remove_Editor_Will_Remove_From_Person_EditedBooks_Property()
        {
            var book = new Book();
            var person = new Person();
            book.AddEditor(person);
            book.RemoveEditor(person);
            Assert.That(person.EditedBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearAuthors_Will_Make_Authors_Property_Empty()
        {
            var book = new Book();
            book.AddAuthor(new Person());
            book.ClearAuthors();
            Assert.That(book.Authors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearAuthors_Will_Remove_Book_From_Person_AuthoredBooks()
        {
            var book = new Book();
            var person = new Person();
            book.AddAuthor(person);
            book.ClearAuthors();
            Assert.That(person.AuthoredBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearTranslator_Will_Make_Translators_Property_Empty()
        {
            var book = new Book();
            book.AddTranslator(new Person());
            book.ClearTranslators();
            Assert.That(book.Translators.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearTranslators_Will_Remove_Book_From_Person_TranslatedBooks()
        {
            var book = new Book();
            var person = new Person();
            book.AddTranslator(person);
            book.ClearTranslators();
            Assert.That(person.TranslatedBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearEditors_Will_Make_Editors_Property_Empty()
        {
            var book = new Book();
            book.AddEditor(new Person());
            book.ClearEditors();
            Assert.That(book.Editors.Count(), Is.EqualTo(0));
        }


        [Test]
        public void ClearEditors_Will_Remove_Book_From_Person_EditedBooks()
        {
            var book = new Book();
            var person = new Person();
            book.AddEditor(person);
            book.ClearEditors();
            Assert.That(person.EditedBooks.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Can_Add_Story()
        {
            var book = new Book();
            book.AddStory(new Story());
            Assert.That(book.Stories.Any());
        }


        [Test]
        public void Add_Story_Will_Set_Position_In_Book()
        {
            var book = new Book();
            book.AddStory(new Story());
            book.AddStory(new Story());
            Assert.That(book.Stories.First().Value.PositionInBook, Is.EqualTo(0));
            Assert.That(book.Stories.Skip(1).First().Value.PositionInBook, Is.EqualTo(1));
        }


        [Test]
        public void Add_Story_Will_Set_Book_On_Story()
        {
            var book = new Book();
            book.AddStory(new Story());
            Assert.That(book.Stories.First().Value.Book, Is.EqualTo(book));
        }


        [Test]
        public void Set_ReferenceBook_Will_Change_Originals_References()
        {
            var original = new Book("The Hobbit") {IsbnLanguage = 1};
            var myBook = new Book("De Hobbit") {IsbnLanguage = 90};

            myBook.ReferenceBook = original;

            Assert.That(original.ReferencedByBooks, Contains.Item(myBook));
        }
    }
}
