using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public class PersonPersistanceTests : EntityPersistanceTests<Person>
    {
        protected override Person GetTypicalEntity()
        {
            return new Person
            {
                Title = "Dr.",
                FirstName = "Augusto",
                Prefix = "von",
                Surname = "Gesaffelstein",
                Gender = Gender.Male,
                Nationality = "US",
                PlaceOfDeath = "Berlin",
                PlaceOfBirth = "Hamburg",
                DateOfBirth = new UncertainDate(1950),
                DateOfDeath = new UncertainDate(1998, 12),
                ReviewText = "Great man!"
            };
        }

        protected override void AssertEntityBasePropertiesAreEqual(Person actual, Person expected)
        {
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.FirstName, Is.EqualTo(expected.FirstName));
            Assert.That(actual.Prefix, Is.EqualTo(expected.Prefix));
            Assert.That(actual.Surname, Is.EqualTo(expected.Surname));
            Assert.That(actual.Gender, Is.EqualTo(expected.Gender));
            Assert.That(actual.Nationality, Is.EqualTo(expected.Nationality));
            Assert.That(actual.PlaceOfDeath, Is.EqualTo(expected.PlaceOfDeath));
            Assert.That(actual.PlaceOfBirth, Is.EqualTo(expected.PlaceOfBirth));
            Assert.That(actual.DateOfBirthFrom, Is.EqualTo(expected.DateOfBirthFrom));
            Assert.That(actual.DateOfBirthUntil, Is.EqualTo(expected.DateOfBirthUntil));
            Assert.That(actual.DateOfDeathFrom, Is.EqualTo(expected.DateOfDeathFrom));
            Assert.That(actual.DateOfDeathUntil, Is.EqualTo(expected.DateOfDeathUntil));
            Assert.That(actual.ReviewText, Is.EqualTo(expected.ReviewText));
        }
    }
}
