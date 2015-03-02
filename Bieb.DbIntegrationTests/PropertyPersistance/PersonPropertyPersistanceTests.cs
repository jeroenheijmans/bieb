using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.PropertyPersistance
{
    [TestFixture]
    public class PersonPropertyPersistanceTests : DatabaseIntegrationTest
    {
        [Test]
        public void Can_Persist_DoB_From_MiddleAges()
        {
            var person = new Person {DateOfBirth = new UncertainDate(1600, 1, 1)};
            Session.Save(person);
            Session.Refresh(person);
            Assert.That(person.DateOfBirth, Is.EqualTo(new UncertainDate(1600, 1, 1)));
        }
    }
}
