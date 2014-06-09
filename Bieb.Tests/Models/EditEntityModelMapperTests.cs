using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.People;
using NUnit.Framework;

namespace Bieb.Tests.Models
{
    [TestFixture]
    public class EditEntityModelMapperTests
    {
        private IEditEntityModelMapper<Person, EditPersonModel> mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new EditPersonModelMapper();
        }
            
            
        [Test]
        public void Will_Guard_Against_Null_Entity_When_Merging()
        {
            var model = new EditPersonModel();
            Assert.Throws<ArgumentNullException>(() => mapper.MergeEntityWithModel(null, model));
        }


        [Test]
        public void Will_Guard_Against_Null_Model_When_Merging()
        {
            var entity = new Person();
            Assert.Throws<ArgumentNullException>(() => mapper.MergeEntityWithModel(entity, null));
        }


        [Test]
        public void Will_Set_ModifiedDate_When_Model_Has_ModifiedDate()
        {
            var entity = new Person();
            var model = new EditPersonModel { ModifiedDateTicks = 1 };
            mapper.MergeEntityWithModel(entity, model);

            Assert.That(entity.ModifiedDate.HasValue);
        }


        [Test]
        public void Will_Leave_ModifiedDate_Null_When_Model_Has_No_ModifiedDate()
        {
            var entity = new Person();
            var model = new EditPersonModel { ModifiedDateTicks = null };
            mapper.MergeEntityWithModel(entity, model);

            Assert.That(!entity.ModifiedDate.HasValue);
        }
    }
}
