using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Models;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditBookModelMapperTests
    {
        private Publisher somePublisher;
        private IEntityRepository<Publisher> publishers;
        private IEntityRepository<Person> people;
        private EditBookModelMapper mapper;
        private Person asimov;
        private Person adams;
        private Person wyndham;

        
        [SetUp]
        public void SetUp()
        {
            somePublisher = new Publisher { Id = 42, Name = "Penguin Books" };
            publishers = new RepositoryMock<Publisher>();
            people = new RepositoryMock<Person>();
            mapper = new EditBookModelMapper(publishers, people);

            asimov = new Person {Id = 1, FirstName = "Isaac", Surname = "Asimov"};
            adams = new Person {Id = 2, FirstName = "Douglas", Surname = "Adams"};
            wyndham = new Person {Id = 3, FirstName = "John", Surname = "Wyndham"};
        }


        [Test]
        public void Will_Map_Publishers_To_Available_Publishers_Dictionary()
        {
            publishers.Add(somePublisher);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePublishers.Count(), Is.EqualTo(1));
            Assert.That(model.AvailablePublishers.First().Value, Is.EqualTo(somePublisher.Id.ToString()));
            Assert.That(model.AvailablePublishers.First().Text, Is.EqualTo(somePublisher.Name));
        }


        [Test]
        public void Will_Set_Null_Publisher_If_Model_Contains_NonExistent_PublisherId()
        {
            var model = new EditBookModel {PublisherId = -1};
            var entity = new Book();

            mapper.MergeEntityWithModel(entity, model);

            Assert.That(entity.Publisher, Is.Null);
        }


        [Test]
        public void Will_Set_Publisher_If_Model_Contains_Some_PublisherId()
        {
            var model = new EditBookModel() { PublisherId = somePublisher.Id };
            var entity = new Book();

            publishers.Add(somePublisher);

            mapper.MergeEntityWithModel(entity, model);

            Assert.That(entity.Publisher, Is.Not.Null);
            Assert.That(entity.Publisher.Id, Is.EqualTo(somePublisher.Id));
        }


        [Test]
        public void Will_Sort_Available_Publishers()
        {
            var bruna = new Publisher {Id = 1, Name = "Bruna"};
            var alpha = new Publisher {Id = 2, Name = "Alpha books"};
            var calda = new Publisher {Id = 3, Name = "Calda ltd."};

            publishers.Add(bruna);
            publishers.Add(alpha);
            publishers.Add(calda);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePublishers.Skip(0).First().Value, Is.EqualTo(alpha.Id.ToString()));
            Assert.That(model.AvailablePublishers.Skip(1).First().Value, Is.EqualTo(bruna.Id.ToString()));
            Assert.That(model.AvailablePublishers.Skip(2).First().Value, Is.EqualTo(calda.Id.ToString()));
        }


        [Test]
        public void Will_Have_Available_Authors()
        {
            people.Add(asimov);
            people.Add(adams);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePeople.Count(), Is.EqualTo(2));

            var includedIds = model.AvailablePeople.Select(p => p.Value);

            Assert.That(includedIds, Contains.Item(asimov.Id.ToString()));
            Assert.That(includedIds, Contains.Item(adams.Id.ToString()));
        }


        [Test]
        public void Will_Sort_Available_Authors()
        {
            people.Add(wyndham);
            people.Add(asimov);
            people.Add(adams);

            var model = mapper.ModelFromEntity(new Book());

            Assert.That(model.AvailablePeople.Skip(0).First().Value, Is.EqualTo(adams.Id.ToString()));
            Assert.That(model.AvailablePeople.Skip(1).First().Value, Is.EqualTo(asimov.Id.ToString()));
            Assert.That(model.AvailablePeople.Skip(2).First().Value, Is.EqualTo(wyndham.Id.ToString()));
        }
    }
}
