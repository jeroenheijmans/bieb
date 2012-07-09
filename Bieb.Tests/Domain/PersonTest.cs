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
        #region SetUp / TearDown

        [SetUp]
        public void Init()
        { }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

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
        

        #endregion
    }
}
