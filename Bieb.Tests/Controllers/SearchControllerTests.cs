using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Controllers;
using Bieb.Web.Models;

namespace Bieb.Tests.Controllers
{
    [TestFixture]
    public class SearchControllerTests
    {
        private Mock<IEntityRepository<LibraryBook>> bookRepositoryMock;
        private Mock<IEntityRepository<Person>> peopleRepositoryMock;
        private Mock<IEntityRepository<Story>> storyRepositoryMock;
        private SearchController controller;


        [SetUp]
        public void SetUp()
        {
            bookRepositoryMock = new Mock<IEntityRepository<LibraryBook>>();
            peopleRepositoryMock = new Mock<IEntityRepository<Person>>();
            storyRepositoryMock = new Mock<IEntityRepository<Story>>();
            controller = new SearchController(peopleRepositoryMock.Object, bookRepositoryMock.Object, storyRepositoryMock.Object);
        }


        [Test]
        public void Can_Find_Book_With_Basic_Search()
        {
            // Arrange
            var markTheMartian = new LibraryBook { Title = "Mark the Martian" };
            var martinTheEarthling = new LibraryBook { Title = "Martin the Earthling" };

            bookRepositoryMock.Setup(repo => repo.Items).Returns(new[] { markTheMartian, martinTheEarthling }.AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns(new Person[] {}.AsQueryable());
            storyRepositoryMock.Setup(repo => repo.Items).Returns(new Story[] {}.AsQueryable());

            // Act
            var result = controller.Basic("Mark");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vResult = (ViewResult)result;

            Assert.That(vResult.Model, Is.InstanceOf<BasicSearchResultModel>());

            var searchResults = (BasicSearchResultModel)vResult.Model;

            Assert.That(searchResults.books.Count(), Is.EqualTo(1));
            Assert.That(searchResults.books.ToList()[0], Is.EqualTo(markTheMartian));
        }


        [Test]
        public void Can_Find_Person_With_Basic_Search()
        {
            // Arrange
            var aldiss = new Person { FirstName = "Brian", Surname = "Aldiss" };
            var asimov = new Person { FirstName = "Isaac", Surname = "Asimov" };

            bookRepositoryMock.Setup(repo => repo.Items).Returns(new LibraryBook[] { }.AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns(new[] { aldiss, asimov }.AsQueryable());
            storyRepositoryMock.Setup(repo => repo.Items).Returns(new Story[] { }.AsQueryable());


            // Act
            var result = controller.Basic("asimov");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vResult = (ViewResult)result;

            Assert.That(vResult.Model, Is.InstanceOf<BasicSearchResultModel>());

            var searchResults = (BasicSearchResultModel)vResult.Model;

            Assert.That(searchResults.people.Count(), Is.EqualTo(1));
            Assert.That(searchResults.people.ToList()[0], Is.EqualTo(asimov));
        }


        [Test]
        public void Can_Find_Story_With_Basic_Search()
        {
            // Arrange
            var bundle = new LibraryBook();
            var story1 = new Story { Title = "The first story ever", Book = bundle };
            var story2 = new Story { Title = "The very second story", Book = bundle };
            bundle.Stories.Add(0, story1);
            bundle.Stories.Add(1, story2);

            bookRepositoryMock.Setup(repo => repo.Items).Returns(new LibraryBook[] { }.AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns(new Person[] { }.AsQueryable());
            storyRepositoryMock.Setup(repo => repo.Items).Returns(new[] { story1, story2 }.AsQueryable());


            // Act
            var result = controller.Basic("ever");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vResult = (ViewResult)result;

            Assert.That(vResult.Model, Is.InstanceOf<BasicSearchResultModel>());

            var searchResults = (BasicSearchResultModel)vResult.Model;

            Assert.That(searchResults.stories.Count(), Is.EqualTo(1));
            Assert.That(searchResults.stories.ToList()[0], Is.EqualTo(story1));
        }


        [Test]
        public void Will_Not_Show_Stories_From_Novels_With_Basic_Search()
        {
            // Arrange
            var novel = new LibraryBook { };
            var story = new Story { Title = "Best book ever", Book = novel };
            novel.Stories.Add(0, story);

            bookRepositoryMock.Setup(repo => repo.Items).Returns(new LibraryBook[] { }.AsQueryable());
            peopleRepositoryMock.Setup(repo => repo.Items).Returns(new Person[] { }.AsQueryable());
            storyRepositoryMock.Setup(repo => repo.Items).Returns(new[] { story }.AsQueryable());


            // Act
            var result = controller.Basic("ever");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var vResult = (ViewResult)result;

            Assert.That(vResult.Model, Is.InstanceOf<BasicSearchResultModel>());

            var searchResults = (BasicSearchResultModel)vResult.Model;

            Assert.That(searchResults.stories.Count(), Is.EqualTo(0));
        }
    }
}
