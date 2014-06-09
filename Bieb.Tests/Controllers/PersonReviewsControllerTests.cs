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
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class PersonReviewsControllerTests
    {
        private IEntityRepository<Review<Person>> repository;
        private PersonReviewsController controller;


        [SetUp]
        public void SetUp()
        {
            repository = new RepositoryMock<Review<Person>>();

            var viewMapper = new ViewPersonReviewModelMapper();
            var editMapper = new Mock<IEditEntityModelMapper<Review<Person>, EditPersonReviewModel>>();

            controller = new PersonReviewsController(repository, viewMapper, editMapper.Object);
        }


        [Test]
        public void Index_Will_Sort_By_Rating_Descending()
        {
            repository.Add(new Review<Person> { Id = 1, Rating = 5, Subject = new Person()});
            repository.Add(new Review<Person> { Id = 2, Rating = 10, Subject = new Person() });

            var result = (ViewResult)controller.Index();
            var model = (IEnumerable<ViewPersonReviewModel>)result.Model;

            Assert.That(model.First().Id, Is.EqualTo(2));
            Assert.That(model.Second().Id, Is.EqualTo(1));
        }
    }
}
