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
        public void Can_Generate_Active_CssClass_For_Book_On_Story_Controller()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Story");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Story link"), routeData);

            var cssClass = htmlHelper.GetCssClass("Books");

            Assert.That(cssClass, Is.EqualTo("active"));
        }


        [Test]
        public void Can_Generate_Active_CssClass_For_Book_On_Stories_Controller()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Stories");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Story link"), routeData);
            
            var cssClass = htmlHelper.GetCssClass("Books");

            Assert.That(cssClass, Is.EqualTo("active"));
        }


        [Test]
        public void Can_Generate_Active_CssClass_For_Current_Controller()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            var cssClass = htmlHelper.GetCssClass("Home");

            Assert.That(cssClass, Is.EqualTo("active"));
        }


        [Test]
        public void Will_Not_Generate_Active_CssClass_For_NonCurrent_Controller()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");

            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), routeData);

            var cssClass = htmlHelper.GetCssClass("Books");

            Assert.That(cssClass, Is.Not.StringContaining("active"));
        }
    }

}
