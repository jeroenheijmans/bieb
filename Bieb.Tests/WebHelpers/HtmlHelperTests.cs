using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using System.Web.Routing;
using Moq;
using System.Web;
using System.IO;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public abstract class HtmlHelperTests
    {
        protected static HtmlHelper<string> CreateHtmlHelper(ViewDataDictionary viewData, RouteData routeData)
        {
            var controllerContext = new Mock<ControllerContext>(
                new Mock<HttpContextBase>().Object,
                routeData,
                new Mock<ControllerBase>().Object);

            var mockViewContext = new Mock<ViewContext>(
                controllerContext.Object,
                new Mock<IView>().Object,
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = new Mock<IViewDataContainer>();

            mockViewContext.Setup(v => v.RouteData).Returns(routeData);
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            var htmlHelper = new HtmlHelper<string>(mockViewContext.Object, mockViewDataContainer.Object);

            Assert.That(htmlHelper, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext.RouteData, Is.Not.Null);

            return htmlHelper;
        }
    }
}
