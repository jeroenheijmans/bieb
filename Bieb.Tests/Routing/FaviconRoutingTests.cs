using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class FaviconRoutingTests : RoutingTests
    {
        [Test]
        [Category("Test")]
        public void Basic_Favicon_Route_Is_Ignored()
        {
            var routeData = GetRouteDataForPath("~/favicon.ico");

            Assert.That(routeData, Is.Not.Null);
            Assert.IsInstanceOf<StopRoutingHandler>(routeData.RouteHandler);
        }

        [Test]
        public void Content_Favicon_Route_Is_Ignored()
        {
            var routeData = GetRouteDataForPath("~/Content/images/favicon.ico");

            Assert.That(routeData, Is.Not.Null);
            Assert.IsInstanceOf<StopRoutingHandler>(routeData.RouteHandler);            
        }
    }
}
