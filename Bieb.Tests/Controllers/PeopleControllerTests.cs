using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Models;
using Bieb.Web.Models.People;
using NUnit.Framework;


namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class PeopleControllerTests
    {
        private IEntityRepository<Person> repository;
        private PeopleController controller;
        private IViewEntityModelMapper<Person, ViewPersonModel> viewEntityModelMapper;
        private IEditEntityModelMapper<Person, EditPersonModel> editEntityModelMapper;

            
        [SetUp]
        public void SetUp()
        {
            repository = new RepositoryMock<Person>();
            viewEntityModelMapper = new ViewPersonModelMapper();
            editEntityModelMapper = new EditPersonModelMapper();
            controller = new PeopleController(repository, viewEntityModelMapper, editEntityModelMapper);
        }


        [Test]
        public void Will_Show_Max_Five_Todays_Birth_Dates()
        {
            for (int i = 0; i < 11; i++)
            {
                repository.Add(new Person { DateOfBirthFrom = DateTime.Now, DateOfBirthUntil = DateTime.Now });
            }

            var result = controller.TodaysBirthDates();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;

            var model = partialViewResult.Model as IEnumerable<LinkablePersonModel>;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(5));
        }


        [Test]
        public void TodaysBirthDates_Will_Skip_People_Without_Birth_Date()
        {
            var dateOfBirth = new DateTime(1950, 11, 11);
            repository.Add(new Person { Id = 42, DateOfBirthFrom = dateOfBirth, DateOfBirthUntil = dateOfBirth });
            repository.Add(new Person { });

            var result = controller.BirthDates(dateOfBirth);

            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;

            var model = partialViewResult.Model as IQueryable<LinkablePersonModel>;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model.First().Id, Is.EqualTo(42));
        }


        [Test]
        public void TodaysBirthDates_Will_Skip_People_With_Uncertain_Birth_Date()
        {
            var dateOfBirthFrom = new DateTime(1950, 11, 11);
            var dateOfBirthUntil = new DateTime(1950, 11, 12);
            var people = new[] { new Person { DateOfBirthFrom = dateOfBirthFrom, DateOfBirthUntil = dateOfBirthUntil },
                                 new Person { } };

            repository = new RepositoryMock<Person>(people);

            var result = controller.BirthDates(dateOfBirthFrom);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PartialViewResult>());

            var partialViewResult = (PartialViewResult)result;

            var model = partialViewResult.Model as IQueryable<LinkablePersonModel>;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(0));
        }
    }
}
