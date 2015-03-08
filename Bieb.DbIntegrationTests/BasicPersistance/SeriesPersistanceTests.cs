using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bieb.Domain.Entities;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public class SeriesPersistanceTests : EntityPersistanceTests<Series>
    {
        protected override Series GetTypicalEntity()
        {
            return new Series
                       {
                           Title = "Nebula Awards",
                           Subtitle = "and other selected stories"
                       };
        }


        protected override void AssertEntityBasePropertiesAreEqual(Series actual, Series expected)
        {
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Subtitle, Is.EqualTo(expected.Subtitle));
        }


        [Test]
        public void Can_Persist_Empty_Series()
        {
            var series = new Series();

            Session.Save(series);
            Session.Flush();
            Session.Refresh(series);

            Assert.That(series.Id, Is.Not.EqualTo(0), "Expected series to get an ID.");
        }


        [Test]
        public void Can_Persist_Series_With_One_Book()
        {
            var series = new Series();
            var book1 = new Book();

            series.AddBook(book1);

            Session.Save(book1);
            Session.Save(series);
            Session.Flush();
            Session.Refresh(series);

            Assert.That(series.Id, Is.Not.EqualTo(0), "Expected series to get an ID.");
            Assert.That(series.Books.Count(), Is.EqualTo(1), "Expected refreshed series to have one book.");
        }


        [Test]
        public void Can_Persist_Series_With_Two_Books()
        {
            var series = new Series();
            var book1 = new Book();
            var book2 = new Book();

            series.AddBook(book1);
            series.AddBook(book2);

            Session.Save(book1);
            Session.Save(book2);
            Session.Save(series);
            Session.Flush();
            Session.Refresh(series);

            Assert.That(series.Id, Is.Not.EqualTo(0), "Expected series to get an ID.");
            Assert.That(series.Books.Count(), Is.EqualTo(2), "Expected refreshed series to have two books.");
        }
    }
}
