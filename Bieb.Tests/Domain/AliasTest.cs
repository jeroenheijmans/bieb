using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class AliasTest
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
            Alias alias = new Alias()
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

            Assert.AreEqual(alias.FullName, expectedFullName);
            Assert.AreEqual(alias.Title, title);
            Assert.AreEqual(alias.FirstName, firstName);
            Assert.AreEqual(alias.Prefix, prefix);
            Assert.AreEqual(alias.Surname, surname);
        }

        [Test]
        public void Can_Create_Simple_Alias()
        {
            // Arrange
            Alias alias = new Alias()
            {
                FirstName = this.firstName,
                Surname = this.surname
            };

            // Act 
            // ..

            // Assert
            string expectedFullName = firstName + " " + surname;

            Assert.AreEqual(expectedFullName, alias.FullName);
            Assert.IsNull(alias.Title);
            Assert.AreEqual(firstName, alias.FirstName);
            Assert.IsNull(alias.Prefix);
            Assert.AreEqual(surname, alias.Surname);
        }
        

        #endregion
    }
}
