using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Bieb.Web.Controllers;
using Bieb.Framework.Logging;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class ErrorControllerTests
    {
        private ErrorController controller;
        private Mock<HttpResponseBase> responseMock;
        private Mock<HttpContextBase> contextMock;


        [SetUp]
        public void SetUp()
        {
            responseMock = new Mock<HttpResponseBase>();
            contextMock = new Mock<HttpContextBase>();
            controller = new ErrorController(responseMock.Object, contextMock.Object);

            responseMock.SetupProperty(response => response.StatusCode);
        }


        [Test]
        public void Http404_Will_Set_Response_Code()
        {
            controller.PageNotFound("");
            Assert.That(responseMock.Object.StatusCode, Is.EqualTo(404));
        }
    }
}
