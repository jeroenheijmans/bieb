using System.Web;
using System.Web.Routing;
using Bieb.Web.App_Start;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public abstract class RoutingTests
    {
        protected RouteData GetRouteDataForPath(string path)
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(context => context.Request.AppRelativeCurrentExecutionFilePath).Returns(path);

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            return routeData;
        }
    }
}
