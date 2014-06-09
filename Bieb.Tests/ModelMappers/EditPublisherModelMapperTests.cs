using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.Publishers;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditPublisherModelMapperTests
    {
        private IEditEntityModelMapper<Publisher, EditPublisherModel> mapper;


        [SetUp]
        public void SetUp()
        {
            mapper = new EditPublisherModelMapper();
        }


        [Test]
        public void Can_Map_Name()
        {
            var publisher = new Publisher {Name = "Bruna"};
            var model = mapper.ModelFromEntity(publisher);
            Assert.That(model.Name, Is.EqualTo(publisher.Name));
        }


        [Test]
        public void Can_Merge_Name_Back_Into_Entity()
        {
            var publisher = new Publisher();
            var model = new EditPublisherModel {Name = "Ace Books"};
            mapper.MergeEntityWithModel(publisher, model);
            Assert.That(publisher.Name, Is.EqualTo(model.Name));
        }
    }
}
