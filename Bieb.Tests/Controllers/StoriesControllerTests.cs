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
        private RepositoryMock<Story> repository;
        private Mock<IEditEntityModelMapper<Story, EditStoryModel>> editMapperMock;


        [SetUp]
        public void SetUp()
        {
            var viewMapperMock = new Mock<IViewEntityModelMapper<Story, ViewStoryModel>>();
            
            editMapperMock = new Mock<IEditEntityModelMapper<Story, EditStoryModel>>();

            repository = new RepositoryMock<Story>();
            controller = new StoriesController(repository, viewMapperMock.Object, editMapperMock.Object);
        }


        [Test]
        public void Index_Will_Redirect_To_Books()
        {
            var result = controller.Index();
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }


        [Test]
        public void Save_Will_Call_NotifyItemWasChanged()
        {
            var story = new Story {Id = 42};
            var model = new EditStoryModel {Id = 42};
            repository.Add(story);
            controller.Save(model);
            Assert.That(repository.NotifyItemWasChangedLastCalledWith, Is.EqualTo(story));
        }
    }
}
