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


        [Test]
        public void ToString_Will_Return_Name()
        {
            var entity = new Publisher {Name = "Xyz"};
            Assert.That(entity.ToString(), Is.EqualTo(entity.Name));
        }


        [Test]
        public void ToString_Can_Handle_Null_Name()
        {
            var entity = new Publisher {Name = null};
            Assert.That(entity.ToString(), Is.Not.Null.Or.Empty);
        }
    }
}
