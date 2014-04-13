using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace DbIntegrationTests.DatabaseObjects
{
    // This class might be mergeable into PublishableEntityDatabaseObjectsTests, if the series
    // and book/story would share an inheritance ancestor that has the Title and Subtitle property.
    // Not sure if that makes too much sense though: giving Series an ancestor that Book has as well.

    [TestFixture]
    public class SeriesDatabaseObjectsTests : DatabaseIntegrationTest
    {
        [Test]
        public void Computed_Title_Column_Can_Handle_Zero_Length_Title()
        {
            Assert.DoesNotThrow(() =>
            {
                var item = new Series { Title = "" };
                Session.Save(item);
            });
        }


        [Test]
        public void Computed_Title_Column_Can_Handle_Short_Title()
        {
            Assert.DoesNotThrow(() =>
            {
                var item = new Series { Title = "A" };
                Session.Save(item);
            });
        }
    }
}
