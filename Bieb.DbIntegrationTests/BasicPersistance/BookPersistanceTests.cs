using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public class BookPersistanceTests : EntityPersistanceTests<Book>
    {
        protected override Book GetTypicalEntity()
        {
            return new Book
                       {
                           Title = "The Long Haul",
                           Subtitle = "and the road ahead",
                           LibraryStatus = LibraryStatus.OnWishlist,
                           Isbn = "0123456789",
                           IsbnLanguage = 1,
                           Year = 2001,
                           ReviewText = "Great book!"
                       };
        }

        protected override void AssertEntityBasePropertiesAreEqual(Book actual, Book expected)
        {
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Subtitle, Is.EqualTo(expected.Subtitle));
            Assert.That(actual.LibraryStatus, Is.EqualTo(expected.LibraryStatus));
            Assert.That(actual.Isbn, Is.EqualTo(expected.Isbn));
            Assert.That(actual.IsbnLanguage, Is.EqualTo(expected.IsbnLanguage));
            Assert.That(actual.Year, Is.EqualTo(expected.Year));
            Assert.That(actual.ReviewText, Is.EqualTo(expected.ReviewText));
        }
    }
}
