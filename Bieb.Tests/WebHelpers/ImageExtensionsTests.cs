using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bieb.Web.Helpers;
using NUnit.Framework;

namespace Bieb.Tests.WebHelpers
{
    [TestFixture]
    public class ImageExtensionsTests : HtmlHelperTests
    {
        private HtmlHelper htmlHelper;


        [SetUp]
        public void SetUp()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Story");
            routeData.Values.Add("action", "Index");

            htmlHelper = CreateHtmlHelper(new ViewDataDictionary("Story link"), routeData);
        }


        [Test]
        public void ImageFlagFor_Will_Return_Img_Tag()
        {
            var result = htmlHelper.ImageFlagFor("NL").ToString();
            Assert.That(result.StartsWith("<img "));
            Assert.That(result.EndsWith("/>"));
        }


        [Test]
        public void ImageFlagFor_Will_Set_Alt_Attribute()
        {
            var result = htmlHelper.ImageFlagFor("NL").ToString();
            StringAssert.IsMatch("alt=['\"].+['\"]", result);
        }


        [Test]
        public void ImageFlagFor_Will_Set_Title_Attribute()
        {
            var result = htmlHelper.ImageFlagFor("NL").ToString();
            StringAssert.IsMatch("title=['\"].+['\"]", result);
        }


        [Test]
        public void ImageFlagFor_Will_Set_Class_Attribute()
        {
            var result = htmlHelper.ImageFlagFor("NL").ToString();
            StringAssert.Contains("class=\"flag icon\"", result);
        }

        
        [Test]
        public void ImageFlagFor_Will_Set_Src_Attribute_To_Some_Png()
        {
            var result = htmlHelper.ImageFlagFor("NL").ToString();
            StringAssert.IsMatch("src=['\"].+\\.png['\"]", result);
        }
    }
}
