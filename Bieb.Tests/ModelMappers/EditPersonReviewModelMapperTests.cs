using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Models;
using Bieb.Web.Models.People;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditPersonReviewModelMapperTests
    {
        private IEntityRepository<Person> repository;
        private IEditEntityModelMapper<Review<Person>, EditPersonReviewModel> mapper;

        [SetUp]
        public void SetUp()
        {
            repository = new RepositoryMock<Person>();
            mapper = new EditPersonReviewModelMapper(repository);
        }


        [Test]
        public void Will_Guard_Against_Null_Repository()
        {
            Assert.Throws<ArgumentNullException>(() => new EditPersonReviewModelMapper(null));
        }


        [Test]
        public void Can_Set_Subject()
        {
            var review = new Review<Person> { Subject = new Person { Id = 42 } };
            var model = mapper.ModelFromEntity(review);
            Assert.That(model.PersonId, Is.EqualTo(review.Subject.Id));
        }


        [Test]
        public void Can_Merge_Subject_Based_On_Model_PersonId()
        {
            repository.Add(new Person { Id = 42 });
            var model = new EditPersonReviewModel { PersonId = 42 };
            var review = new Review<Person>();
            mapper.MergeEntityWithModel(review, model);
            Assert.That(review.Subject.Id, Is.EqualTo(42));
        }


        [Test]
        public void Will_Throw_Mapping_Exception_For_Unkown_PersonId()
        {
            var model = new EditPersonReviewModel { PersonId = 1234 };
            var review = new Review<Person>();
            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(review, model));
        }
    }
}
