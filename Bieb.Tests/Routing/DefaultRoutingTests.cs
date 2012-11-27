using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using System.Web.Routing;
using Bieb.Web;
using Moq;
using System.Web;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class DefaultRoutingTests : RoutingTests
    {
        [Test]
        public void Default_Route_Leads_To_Home_Index()
        {
            var routeData = GetRouteDataForPath("~/");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Home"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Home_Leads_To_Home_Index()
        {
            var routeData = GetRouteDataForPath("~/Home");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Home"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Home_Index_Leads_To_Home_Index()
        {
            var routeData = GetRouteDataForPath("~/Home/Index");

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["Controller"], Is.EqualTo("Home"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
        }
    }
}
