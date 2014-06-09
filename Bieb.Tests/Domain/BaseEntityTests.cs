using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class BaseEntityTests
    {
        [Test]
        public void Null_Will_Not_Equal_Object()
        {
            var book = new Book();
            Assert.That(book.Equals(null), Is.False);
        }


        [Test]
        public void Entity_Will_Equal_Self()
        {
            var book = new Book();
            Assert.That(book.Equals(book), Is.True);
        }


        [Test]
        public void Entity_Will_Use_ReferenceEquals_For_Transient_Objects()
        {
            var book1 = new Book();
            var book2 = new Book();
            Assert.That(book1.Equals(book2), Is.False);
        }


        [Test]
        public void Entity_Will_Check_Id_Equality_For_Persisted_Objects()
        {
            var proxy1 = new Book() {Id = 1};
            var proxy2 = new Book() {Id = 1}; // Simulate proxy for the same entity.

            Assert.That(proxy1.Equals(proxy2));
        }
    }
}
