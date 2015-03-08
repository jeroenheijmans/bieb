using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.BasicPersistance
{
    public abstract class EntityPersistanceTests<T> : DatabaseIntegrationTest where T : BaseEntity, new()
    {
        /// <summary>
        /// Get a typical entity instance with simple properties populated.
        /// </summary>
        protected abstract T GetTypicalEntity();


        /// <summary>
        /// Should do an Assert for every simple property on the entity.
        /// </summary>
        protected abstract void AssertEntityBasePropertiesAreEqual(T actual, T expected);


        [Test]
        public void Can_Persist_Default_Entity()
        {
            var entity = new T();
            Session.Save(entity);
            Session.Flush();
            Session.Refresh(entity);
            Assert.That(entity.Id, Is.Not.EqualTo(0), "Expecting entity of type {0} to get an ID.", typeof(T).Name);
        }


        [Test]
        [Category("SmokeTest")]
        public void Can_Persist_Typical_Entity()
        {
            var entity = GetTypicalEntity();

            Session.Save(entity);
            Session.Flush();
            Session.Refresh(entity);

            var expected = GetTypicalEntity();

            AssertEntityBasePropertiesAreEqual(entity, expected);
        }
    }
}
