using System;
using NUnit.Framework;
using Bieb.Domain.CustomDataTypes;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class UncertainDateTests
    {
        [Test]
        public void Can_Be_Constructed_Without_Any_Info()
        {
            var date = new UncertainDate();

            Assert.That(date.Day, Is.Null);
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Year, Is.Null);
        }


        [Test]
        public void Can_Be_Constructed_With_Only_Year_Known()
        {
            var date = new UncertainDate(1999);

            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Day, Is.Null);
        }


        [Test]
        public void Can_Be_Constructed_With_Full_Date_Info()
        {
            var date = new UncertainDate(1999, 11, 23);

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
            var from = new DateTime(1999, 1, 1);
            var until = new DateTime(1999, 12, 31);
            var date = new UncertainDate(from, until);

            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Null);
            Assert.That(date.Day, Is.Null);
        }


        [Test]
        public void Can_Be_Constructed_With_One_Month_Span()
        {
            var from = new DateTime(1999, 6, 1);
            var until = new DateTime(1999, 6, 30);
            var date = new UncertainDate(from, until);

            Assert.That(date.Year, Is.Not.Null);
            Assert.That(date.Year.Value, Is.EqualTo(1999));
            Assert.That(date.Month, Is.Not.Null);
            Assert.That(date.Month.Value, Is.EqualTo(6));
            Assert.That(date.Day, Is.Null);
        }


        [Test]
        public void Completely_Uncertain_Date_Gives_Question_Mark_On_ToString()
        {
            var date = new UncertainDate();

            Assert.That(date.ToString(), Is.EqualTo("?"));
        }


        [Test]
        public void Only_Year_Known_Gives_Year_On_ToString()
        {
            var date = new UncertainDate(1998);

            Assert.That(date.ToString(), Is.EqualTo("1998"));
        }


        [Test]
        [SetCulture("en-US")]
        public void Only_Year_And_Month_Known_Gives_Localized_Monthname_Plus_Year_On_ToString()
        {
            var date = new UncertainDate(1998, 1);

            Assert.That(date.ToString(), Is.EqualTo("January 1998"));
        }


        [Test]
        [SetCulture("en-US")]
        public void Known_Date_Gives_Localized_DateTime_ShortDateString_On_ToString()
        {
            var someDateTime = new DateTime(1992, 4, 6);
            var date = new UncertainDate(someDateTime, someDateTime);

            Assert.That(date.ToString(), Is.EqualTo(someDateTime.ToShortDateString()));
        }


        [Test]
        public void Construct_From_Certain_Dates_Gives_Back_Certain_Dates()
        {
            var someDateTime = new DateTime(1780, 4, 20);
            var uncertainDate = new UncertainDate(someDateTime, someDateTime);

            var fromDate = uncertainDate.FromDate;
            var toDate = uncertainDate.UntilDate;

            Assert.That(someDateTime, Is.EqualTo(fromDate));
            Assert.That(someDateTime, Is.EqualTo(toDate));
        }


        [Test]
        public void Uncertain_Date_With_Only_Year_Will_Give_Back_To_And_From_Spanning_One_Year()
        {
            const int year = 1950;
            var uncertainDate = new UncertainDate(year, null, null);

            var fromDate = uncertainDate.FromDate;
            var toDate = uncertainDate.UntilDate;

            Assert.That(fromDate.HasValue);
            Assert.That(fromDate.Value.Year, Is.EqualTo(year));
            Assert.That(fromDate.Value.Month, Is.EqualTo(1));
            Assert.That(fromDate.Value.Day, Is.EqualTo(1));

            Assert.That(toDate.HasValue);
            Assert.That(toDate.Value.Year, Is.EqualTo(year));
            Assert.That(toDate.Value.Month, Is.EqualTo(12));
            Assert.That(toDate.Value.Day, Is.EqualTo(31));
        }


        [Test]
        public void Can_Create_Leap_Year_Uncertain_Date_For_February_Without_Day()
        {
            const int leapyear = 1600;
            const int february = 2;
            var uncertainDate = new UncertainDate(leapyear, february, null);

            var fromDate = uncertainDate.FromDate;
            var toDate = uncertainDate.UntilDate;

            Assert.That(fromDate.HasValue);
            Assert.That(fromDate.Value.Year, Is.EqualTo(leapyear));
            Assert.That(fromDate.Value.Month, Is.EqualTo(february));
            Assert.That(fromDate.Value.Day, Is.EqualTo(1));

            Assert.That(toDate.HasValue);
            Assert.That(toDate.Value.Year, Is.EqualTo(leapyear));
            Assert.That(toDate.Value.Month, Is.EqualTo(february));
            Assert.That(toDate.Value.Day, Is.EqualTo(29));
        }
    }
}
