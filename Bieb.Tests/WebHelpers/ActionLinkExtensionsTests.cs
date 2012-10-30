using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Bieb.Web.Helpers;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public class ActionLinkExtensionsTest
    {
        [Test]
        public void MenuLink_Sets_Class_To_Active()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            Assert.That(htmlHelper, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext.RouteData, Is.Not.Null);

            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "Home";

            // Act
            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }

        [Test]
        public void MenuLink_For_Story_Sets_Book_To_Active()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Story");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            Assert.That(htmlHelper, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext.RouteData, Is.Not.Null);

            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "Book";

            // Act
            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }

        private static HtmlHelper<string> CreateHtmlHelper(ViewDataDictionary viewData, RouteData routeData)
        {
            var cc = new Mock<ControllerContext>(
                new Mock<HttpContextBase>().Object,
                routeData,
                new Mock<ControllerBase>().Object);

            var mockViewContext = new Mock<ViewContext>(
                cc.Object,
                new Mock<IView>().Object,
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = new Mock<IViewDataContainer>();

            mockViewContext.Setup(v => v.RouteData).Returns(routeData);
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            var h = new HtmlHelper<string>(mockViewContext.Object, mockViewDataContainer.Object);
            return h;
        }
    }

}
