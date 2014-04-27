using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.Cascades
{
    [TestFixture]
    public class PersonCascadeTests : DatabaseIntegrationTest
    {
        private Person testPerson;
        private Book testBook;
        private Story testStory;


        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            testPerson = new Person {FirstName = "Douglas", Surname = "Adams"};
            testBook = new Book {Title = "March of the Machines"};
            testStory = new Story {Title = "The first tale"};
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Authored_Books()
        {
            testPerson.AddAuthoredBook(testBook);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testPerson);
                Session.Save(testBook);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            Assert.DoesNotThrow(() => TransactionWrappedDelete(testPerson));
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Edited_Books()
        {
            testPerson.AddEditedBook(testBook);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testPerson);
                Session.Save(testBook);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            Assert.DoesNotThrow(() => TransactionWrappedDelete(testPerson));
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Translated_Books()
        {
            testPerson.AddTranslatedBook(testBook);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testPerson);
                Session.Save(testBook);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            Assert.DoesNotThrow(() => TransactionWrappedDelete(testPerson));
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Authored_Stories()
        {
            testPerson.AddAuthoredStory(testStory);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testPerson);
                Session.Save(testStory);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            Assert.DoesNotThrow(() => TransactionWrappedDelete(testPerson));
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Translated_Stories()
        {
            testPerson.AddTranslatedStory(testStory);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testPerson);
                Session.Save(testStory);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            Assert.DoesNotThrow(() => TransactionWrappedDelete(testPerson));
        }


        private void TransactionWrappedDelete(BaseEntity entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}
