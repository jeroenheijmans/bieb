using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    public class AccountRoutingTests : RoutingTests
    {
        [Test]
        public void Account_Default_Route_Is_Manage_Account_Info()
        {
            var routeData = this.GetRouteDataForPath("~/Account");

            Assert.That(routeData.Values["Controller"], Is.EqualTo("Account"));
            Assert.That(routeData.Values["Action"], Is.EqualTo("Manage"));
        }

        [Test]
        public void Explicit_Login_Url_Will_Lead_To_Account_Login_Action()
        {
            var routeData = GetRouteDataForPath("~/Account/Login");

            Assert.That(routeData.Values["Controller"], Is.EqualTo("Account"));
            Assert.That(routeData.Values["Action"], Is.EqualTo("Login"));
        }

        [Test]
        public void Explicit_LogOff_Url_Will_Lead_To_Account_LogOff_Action()
        {
            var routeData = GetRouteDataForPath("~/Account/LogOff");

            Assert.That(routeData.Values["Controller"], Is.EqualTo("Account"));
            Assert.That(routeData.Values["Action"], Is.EqualTo("LogOff"));
        }

        [Test]
        public void Explicit_Register_Url_Will_Lead_To_Account_Register_Action()
        {
            var routeData = GetRouteDataForPath("~/Account/Register");

            Assert.That(routeData.Values["Controller"], Is.EqualTo("Account"));
            Assert.That(routeData.Values["Action"], Is.EqualTo("Register"));
        }
    }
}
