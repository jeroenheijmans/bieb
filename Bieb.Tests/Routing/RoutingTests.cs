using NUnit.Framework;
using System.Web.Routing;
using Bieb.Web;
using Moq;
using System.Web;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public abstract class RoutingTests
    {
        protected RouteData GetRouteDataForPath(string path)
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(context => context.Request.AppRelativeCurrentExecutionFilePath).Returns(path);

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            return routeData;
        }
    }
}
