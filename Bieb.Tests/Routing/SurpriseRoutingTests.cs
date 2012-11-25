using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class SurpriseRoutingTests : RoutingTests
    {
        [Test]
        public void Surprise_Will_Lead_To_Index()
        {
            var routeData = GetRouteDataForPath("~/Surprise");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Surprise"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
        }
    }
}
