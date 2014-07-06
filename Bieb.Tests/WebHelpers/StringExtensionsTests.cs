using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Web.Helpers;
using NUnit.Framework;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public class StringExtensionsTests : HtmlHelperTests
    {
        [Test]
        public void Can_Wrap_Simple_Text_In_Header()
        {
            var helper = CreateHtmlHelper(new ViewDataDictionary("Story link"), new RouteData());
            var result = helper.ShowIfNotNull("text", "h2");
            Assert.That(result.ToHtmlString(), Is.EqualTo("<h2>text</h2>"));
        }
    }
}
