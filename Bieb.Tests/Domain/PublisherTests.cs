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
        public void Books_Is_MyBooks_And_ReferenceBooks_Joined()
        {
            var book1 = new Book{ LibraryStatus = LibraryStatus.InPosession };
            var book2 = new Book { LibraryStatus = LibraryStatus.OnlyForReference };
            var publisher = new Publisher();
            publisher.Books.Add(book1);
            publisher.Books.Add(book2);

            Assert.That(publisher.MyBooks.Count(), Is.EqualTo(1));
            CollectionAssert.Contains(publisher.MyBooks, book1);

            Assert.That(publisher.ReferenceBooks.Count(), Is.EqualTo(1));
            CollectionAssert.Contains(publisher.ReferenceBooks, book2);
        }
    }
}
