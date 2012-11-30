using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Domain.Entities;
using NUnit.Framework;
using Bieb.Web.Helpers;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public class ImageExtensionsTests : HtmlHelperTests
    {
        [Ignore("Need to mock/decouple the VirtualPathUtility to make this test work.")]
        [Test]
        public void Can_Create_Flag_Image_With_Dutch_Png()
        {
            HtmlHelper htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Home link"), new RouteData());

            var result = htmlHelper.ImageFlagFor(new Person() { Nationality = "nl"});

            Assert.That(result, Is.Not.Null);
        }
    }
}
