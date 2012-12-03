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
    public class ActionLinkExtensionsTest : HtmlHelperTests
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
            const string controllerName = "Books";

            // Act
            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }

        [Test]
        public void MenuLink_For_Stories_Sets_Book_To_Active()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Stories");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            Assert.That(htmlHelper, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext, Is.Not.Null);
            Assert.That(htmlHelper.ViewContext.RouteData, Is.Not.Null);

            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "Books";

            // Act
            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }
    }

}
