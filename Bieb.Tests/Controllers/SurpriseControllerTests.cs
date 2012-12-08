﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class SurpriseControllerTests
    {
        [Test]
        public void Index_Will_Redirect_To_Empty_Database_Page_If_No_Books_In_Database()
        {
            // Arrange
            var booksMock = new Mock<IEntityRepository<Book>>();
            booksMock.Setup(repo => repo.Items).Returns((new Book[] { }).AsQueryable());
            var peopleMock = new Mock<IEntityRepository<Person>>();
            peopleMock.Setup(repo => repo.Items).Returns((new Person[] { }).AsQueryable());
            var controller = new SurpriseController(peopleMock.Object, booksMock.Object);

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }
    }
}
