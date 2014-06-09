using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Controllers;
using Bieb.Web.Models;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class StoriesControllerTests
    {
        private StoriesController controller;
        private IEntityRepository<Story> repository;


        [SetUp]
        public void SetUp()
        {
            var viewMapperMock = new Mock<IViewEntityModelMapper<Story, ViewStoryModel>>();
            var editMapperMock = new Mock<IEditEntityModelMapper<Story, EditStoryModel>>();

            repository = new RepositoryMock<Story>();
            controller = new StoriesController(repository, viewMapperMock.Object, editMapperMock.Object);
        }


        [Test]
        public void Index_Will_Redirect_To_Books()
        {
            var result = controller.Index();
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }

    }
}
