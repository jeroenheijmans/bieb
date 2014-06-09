using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Controllers;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController controller;


        [SetUp]
        public void SetUp()
        {
            controller = new HomeController();
        }


        [Test]
        public void Can_Show_Index()
        {
            var result = controller.Index();
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public void Can_Show_EmptyDatabase()
        {
            var result = controller.EmptyDatabase();
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public void Can_Show_About()
        {
            var result = controller.About();
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public void Can_Show_Disclaimer()
        {
            var result = controller.Disclaimer();
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public void Can_Show_SiteMap()
        {
            var result = controller.SiteMap();
            Assert.That(result, Is.Not.Null);
        }
    }
}
