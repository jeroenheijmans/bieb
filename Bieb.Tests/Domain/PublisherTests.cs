using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class PublisherTests
    {
        [Test]
        public void Books_Is_LibraryBooks_And_ReferenceBooks_Joined()
        {
            // Arrange
            var book1 = new LibraryBook();
            var book2 = new ReferenceBook();
            var publisher = new Publisher();
            publisher.LibraryBooks.Add(book1);
            publisher.ReferenceBooks.Add(book2);

            // Act & Assert
            Assert.That(publisher.Books.Count(), Is.EqualTo(2));
            CollectionAssert.Contains(publisher.Books, book1);
            CollectionAssert.Contains(publisher.Books, book2);
        }
    }
}
