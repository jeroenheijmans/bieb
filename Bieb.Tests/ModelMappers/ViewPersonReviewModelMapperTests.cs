using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.People;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewPersonReviewModelMapperTests
    {
        private IViewEntityModelMapper<Review<Person>, ViewPersonReviewModel> mapper;


        [SetUp]
        public void SetUp()
        {
            mapper = new ViewPersonReviewModelMapper();
        }


        [Test]
        public void Can_Map_Subject_AsLinkablePersonModel()
        {
            var review = new Review<Person> { Subject = new Person { Id = 42 } };
            var model = mapper.ModelFromEntity(review);
            Assert.That(model.Person.Id, Is.EqualTo(review.Subject.Id));
        }
    }
}
