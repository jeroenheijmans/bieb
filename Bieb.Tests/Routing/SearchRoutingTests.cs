using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class SearchRoutingTests : RoutingTests
    {
        [Test]
        public void Search_Will_Lead_To_Basic_Search()
        {
            var routeData = GetRouteDataForPath("~/Search");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Search"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Basic"));
        }

        [Test]
        public void Search_Basic_Will_Lead_To_Basic_Search()
        {
            var routeData = GetRouteDataForPath("~/Search/Basic");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Search"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Basic"));
        }
    }
}
