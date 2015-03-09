using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Stories;
using Moq;
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
            var iso639LanguageDisplayer = new Mock<IIso639LanguageDisplayer>().Object;
            var bookMapper = new ViewBookModelMapper(iso639LanguageDisplayer);
            var storyMapper = new ViewStoryModelMapper(iso639LanguageDisplayer);

            repository = new RepositoryMock<Person>();
            viewEntityModelMapper = new ViewPersonModelMapper(bookMapper, storyMapper);
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

            var result = (PartialViewResult)controller.TodaysBirthDates();
            var model = (IEnumerable<LinkablePersonModel>)result.Model;
            Assert.That(model.Count(), Is.EqualTo(5));
        }


        [Test]
        public void TodaysBirthDates_Will_Skip_People_Without_Birth_Date()
        {
            var dateOfBirth = new DateTime(1950, 11, 11);
            repository.Add(new Person { Id = 42, DateOfBirthFrom = dateOfBirth, DateOfBirthUntil = dateOfBirth });
            repository.Add(new Person { });

            var result = (PartialViewResult)controller.BirthDates(dateOfBirth);
            var model = (IQueryable<LinkablePersonModel>)result.Model;

            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model.First().Id, Is.EqualTo(42));
        }


        [Test]
        public void TodaysBirthDates_Will_Skip_People_With_Uncertain_Birth_Date()
        {
            var dateOfBirthFrom = new DateTime(1950, 11, 11);
            var dateOfBirthUntil = new DateTime(1950, 11, 12);

            repository.Add(new Person {DateOfBirthFrom = dateOfBirthFrom, DateOfBirthUntil = dateOfBirthUntil});
            repository.Add(new Person());

            var result = (PartialViewResult)controller.BirthDates(dateOfBirthFrom);
            var model = (IQueryable<LinkablePersonModel>)result.Model;

            Assert.That(model.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Will_Sort_By_Surname()
        {
            repository.Add(new Person {Surname = "Zoltan"});
            repository.Add(new Person {Surname = "Alfa"});

            var result = (ViewResult)controller.Index();
            var model = (IEnumerable<ViewPersonModel>)result.Model;

            Assert.That(model.First().FullName, Is.EqualTo("Alfa"));
            Assert.That(model.Second().FullName, Is.EqualTo("Zoltan"));
        }
    }
}
