using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public class PublisherPersistanceTests : EntityPersistanceTests<Publisher>
    {
        protected override Publisher GetTypicalEntity()
        {
            return new Publisher { Name = "The Long Haul" };
        }

        protected override void AssertEntityBasePropertiesAreEqual(Publisher actual, Publisher expected)
        {
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
        }
    }
}
