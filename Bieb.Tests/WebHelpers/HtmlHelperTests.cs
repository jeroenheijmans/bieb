using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using System.Web.Routing;
using Moq;
using System.Web;
using System.IO;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    // TODO: An abstract class with only one static method? Refactor that.
    public abstract class HtmlHelperTests
    {
        protected static HtmlHelper<string> CreateHtmlHelper(ViewDataDictionary viewData, RouteData routeData)
        {
            var controllerContext = new Mock<ControllerContext>(
                new Mock<HttpContextBase>().Object,
                routeData,
                new Mock<ControllerBase>().Object);

            var httpContext = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();

            httpContext.Setup(h => h.Request).Returns(request.Object);
            request.Setup(r => r.ApplicationPath).Returns("/");
            
            var mockViewContext = new Mock<ViewContext>(
                controllerContext.Object,
                new Mock<IView>().Object,
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = new Mock<IViewDataContainer>();

            mockViewContext.Setup(v => v.RouteData).Returns(routeData);
            mockViewContext.Setup(v => v.HttpContext).Returns(httpContext.Object);
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            var htmlHelper = new HtmlHelper<string>(mockViewContext.Object, mockViewDataContainer.Object);

            Assert.That(htmlHelper.ViewContext, Is.Not.Null, "CreateHtmlHelper didn't set ViewContext properly.");
            Assert.That(htmlHelper.ViewContext.RouteData, Is.Not.Null, "CreateHtmlHelper didn't set ViewContext.RouteData properly.");
            Assert.That(htmlHelper.ViewContext.HttpContext, Is.Not.Null, "CreateHtmlHelper didn't set ViewContext.HttpContext properly.");

            return htmlHelper;
        }
    }
}
