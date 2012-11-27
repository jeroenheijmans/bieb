using System;
using NUnit.Framework;
using Bieb.Domain.CustomDataTypes;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class UncertainDateTests
    {
        #region Tests

        [Test]
        public void Can_Be_Constructed_Without_Any_Info()
        {
            // Arrange
            var date = new UncertainDate();

            // Act & Assert
            Assert.That(date.Day, Is.Null);
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Year, Is.Null);
        }

        [Test]
        public void Can_Be_Constructed_With_Only_Year_Known()
        {
            // Arrange 
            var date = new UncertainDate(1999);

            // Act & Assert
            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Day, Is.Null);
        }

        [Test]
        public void Can_Be_Constructed_With_Full_Date_Info()
        {
            // Arrange 
            var date = new UncertainDate(1999, 11, 23);

            // Act & Assert
            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Month, Is.Not.Null);
            Assert.That(date.Day, Is.Not.Null);

            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month.Value, Is.EqualTo(11));
            Assert.That(date.Day.Value, Is.EqualTo(23));
        }

        [Test]
        public void Can_Be_Constructed_With_One_Year_Span()
        {
            // Arrange
            var from = new DateTime(1999, 1, 1);
            var until = new DateTime(1999, 12, 31);
            var date = new UncertainDate(from, until);

            // Act & Assert
            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Day, Is.Null);
        }

        [Test]
        public void Can_Be_Constructed_With_One_Month_Span()
        {
            // Arrange
            var from = new DateTime(1999, 6, 1);
            var until = new DateTime(1999, 6, 30);
            var date = new UncertainDate(from, until);

            // Act & Assert
            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Not.Null);
            Assert.That(date.Month.Value, Is.EqualTo(6));
            Assert.That(date.Day, Is.Null);
        }

        [Test]
        public void Completely_Uncertain_Date_Gives_Question_Mark_On_ToString()
        {
            // Arrange
            var date = new UncertainDate();

            // Act & Assert
            Assert.That(date.ToString(), Is.EqualTo("?"));
        }

        [Test]
        public void Only_Year_Known_Gives_Year_On_ToString()
        {
            // Arrange
            var date = new UncertainDate(1998);

            // Act & Assert
            Assert.That(date.ToString(), Is.EqualTo("1998"));
        }

        [Test]
        [SetCulture("en-US")]
        public void Only_Year_And_Month_Known_Gives_Localized_Monthname_Plus_Year_On_ToString()
        {
            // Arrange
            var date = new UncertainDate(1998, 1);

            // Act & Assert
            Assert.That(date.ToString(), Is.EqualTo("January 1998"));
        }

        [Test]
        [SetCulture("en-US")]
        public void Known_Date_Gives_Localized_DateTime_ShortDateString_On_ToString()
        {
            // Arrange
            var someDateTime = new DateTime(1992, 4, 6);
            var date = new UncertainDate(someDateTime, someDateTime);

            // Act & Assert
            Assert.That(date.ToString(), Is.EqualTo(someDateTime.ToShortDateString()));
        }

        #endregion
    }
}
