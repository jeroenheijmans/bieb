using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.DatabaseObjects
{
    public class PublishableEntityDatabaseObjectsTests<T> : DatabaseIntegrationTest where T : Publishable, new()
    {
        [Test]
        public void Computed_Title_Column_Can_Handle_Zero_Length_Title()
        {
            Assert.DoesNotThrow(() =>
            {
                var item = new T { Title = "" };
                Session.Save(item);
            });
        }


        [Test]
        public void Computed_Title_Column_Can_Handle_Short_Title()
        {
            Assert.DoesNotThrow(() =>
            {
                var item = new T { Title = "A" };
                Session.Save(item);
            });
        }
    }
}
