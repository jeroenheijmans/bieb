using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public class ActionLinkExtensionsTest : HtmlHelperTests
    {
        [Test]
        public void MenuLink_Sets_Class_To_Active()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "Home";

            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }


        [Test]
        public void MenuLink_For_Story_Sets_Book_To_Active()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Story");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);
            
            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "LibraryBooks";

            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }


        [Test]
        public void MenuLink_For_Stories_Sets_Book_To_Active()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Stories");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);
            
            const string linkText = "clicky, clicky!";
            const string actionName = "Index";
            const string controllerName = "LibraryBooks";

            MvcHtmlString actual = htmlHelper.MenuLink(linkText, actionName, controllerName);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Contains.Substring("class=\"active\""));
        }
    }

}
