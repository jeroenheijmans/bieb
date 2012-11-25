using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public abstract class DomainRoutingTests : RoutingTests
    {
        protected abstract string PrimaryAlias { get; }
        protected virtual IEnumerable<string> Aliases { get { return new[] {PrimaryAlias}; } }

        [Test]
        public void Aliases_Lead_To_Index_Action()
        {
            foreach (var alias in Aliases)
            {
                var routeData = GetRouteDataForPath("~/" + alias);

                Assert.That(routeData, Is.Not.Null);
                Assert.That(routeData.Values["Controller"], Is.EqualTo(PrimaryAlias));
                Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
            }
        }

        [Test]
        public void Aliases_With_Index_Lead_To_Index_Action()
        {
            foreach (var alias in Aliases)
            {
                var routeData = GetRouteDataForPath("~/" + alias + "/Index");

                Assert.That(routeData, Is.Not.Null);
                Assert.That(routeData.Values["Controller"], Is.EqualTo(PrimaryAlias));
                Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
            }
        }

        [Test]
        public void Aliases_With_Details_And_Id_Lead_To_Individual_Item()
        {
            foreach (var alias in Aliases)
            {
                var routeData = GetRouteDataForPath("~/" + alias + "/Details/1");

                Assert.That(routeData, Is.Not.Null);
                Assert.That(routeData.Values["Controller"], Is.EqualTo(PrimaryAlias));
                Assert.That(routeData.Values["action"], Is.EqualTo("Details"));
                Assert.That(routeData.Values["id"], Is.EqualTo("1"));
            }
        }

    }
}
