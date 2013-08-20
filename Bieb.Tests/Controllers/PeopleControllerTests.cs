using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Moq;
using NUnit.Framework;


namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class PeopleControllerTests
    {
        private Mock<IEntityRepository<Person>> repositoryMock;
        private PeopleController controller;

        
        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IEntityRepository<Person>>();
            controller = new PeopleController(repositoryMock.Object);
        }


        [Test]
        public void Will_Show_Max_Five_Todays_Birth_Dates()
        {
            // Arrange
            var people = Enumerable.Repeat(new Person { DateOfBirthFrom = DateTime.Now, DateOfBirthUntil = DateTime.Now }, 10);
            repositoryMock.Setup(repo => repo.Items).Returns(people.AsQueryable());

            // Act
            var result = controller.TodaysBirthDates();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;
            Assert.That(partialViewResult.Model, Is.InstanceOf<IEnumerable<Person>>());

            var model = (IEnumerable<Person>)partialViewResult.Model;
            Assert.That(model.Count(), Is.EqualTo(5));
        }

        [Test]
        public void TodaysBirthDates_Will_Skip_People_Without_Birth_Date()
        {
            // Arrange
            var dateOfBirth = new DateTime(1950, 11, 11);
            var people = new[] { new Person { DateOfBirthFrom = dateOfBirth, DateOfBirthUntil = dateOfBirth },
                                 new Person { } };
            repositoryMock.Setup(repo => repo.Items).Returns(people.AsQueryable());

            // Act
            var result = controller.BirthDates(dateOfBirth);

            // Assert
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;
            Assert.That(partialViewResult.Model, Is.InstanceOf<IEnumerable<Person>>());

            var model = (IEnumerable<Person>)partialViewResult.Model;
            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model.First().DateOfBirthFrom, Is.EqualTo(dateOfBirth));
        }

        [Test]
        public void TodaysBirthDates_Will_Skip_People_With_Uncertain_Birth_Date()
        {
            // Arrange
            var dateOfBirthFrom = new DateTime(1950, 11, 11);
            var dateOfBirthUntil = new DateTime(1950, 11, 12);
            var people = new[] { new Person { DateOfBirthFrom = dateOfBirthFrom, DateOfBirthUntil = dateOfBirthUntil },
                                 new Person { } };
            repositoryMock.Setup(repo => repo.Items).Returns(people.AsQueryable());

            // Act
            var result = controller.BirthDates(dateOfBirthFrom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;
            Assert.That(partialViewResult.Model, Is.InstanceOf<IEnumerable<Person>>());

            var model = (IEnumerable<Person>)partialViewResult.Model;
            Assert.That(model.Count(), Is.EqualTo(0));
        }
    }
}
