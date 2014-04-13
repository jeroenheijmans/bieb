using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace DbIntegrationTests.Cascades
{
    [TestFixture]
    public class PersonCascadeTests : DatabaseIntegrationTest
    {
        private Person testPerson;
        private Book testBook;


        [SetUp]
        public void SetUp()
        {
            testPerson = new Person {FirstName = "Douglas", Surname = "Adams"};
            testBook = new Book {Title = "March of the Machines"};
        }


        [Test]
        public void Save_Person_Will_Cascade_To_BookEditor_Relation()
        {
            testPerson.EditedBooks.Add(testBook);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testBook);
                Session.Save(testPerson);

                transaction.Commit();
            }

            var samePerson = Session.Load<Person>(testPerson.Id);
            Assert.That(samePerson.EditedBooks.Count, Is.EqualTo(1));
        }


        [Test]
        public void Delete_Person_Will_Cascade_To_Authored_Books()
        {
            testBook.BookAuthors.Add(testPerson);

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(testBook);
                Session.Save(testPerson);
                transaction.Commit();
            }

            Session.Refresh(testPerson);

            // Would prefer a generic DoesNotThrow<SqlException> call here though
            Assert.DoesNotThrow(() =>
                                    {
                                        using (var transaction = Session.BeginTransaction())
                                        {
                                            Session.Delete(testPerson);
                                            transaction.Commit();
                                        }
                                    });
        }
    }
}
