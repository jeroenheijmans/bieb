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
        [Test]
        public void Can_Find_Book_With_Basic_Search()
        {
            // Arrange
            var bookMock = new Mock<IEntityRepository<Book>>();
            var personMock = new Mock<IEntityRepository<Person>>();
            var storyMock = new Mock<IEntityRepository<Story>>();

            var markTheMartian = new Book() { Title = "Mark the Martian" };
            var martinTheEarthling = new Book() { Title = "Martin the Earthling" };

            bookMock.Setup(repo => repo.Items).Returns(new Book[] { markTheMartian, martinTheEarthling }.AsQueryable());
            personMock.Setup(repo => repo.Items).Returns(new Person[] {}.AsQueryable());
            storyMock.Setup(repo => repo.Items).Returns(new Story[] {}.AsQueryable());

            var controller = new SearchController(personMock.Object, bookMock.Object, storyMock.Object);

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
            var bookMock = new Mock<IEntityRepository<Book>>();
            var personMock = new Mock<IEntityRepository<Person>>();
            var storyMock = new Mock<IEntityRepository<Story>>();

            var aldiss = new Person() { FirstName = "Brian", Surname = "Aldiss" };
            var asimov = new Person() { FirstName = "Isaac", Surname = "Asimov" };

            bookMock.Setup(repo => repo.Items).Returns(new Book[] { }.AsQueryable());
            personMock.Setup(repo => repo.Items).Returns(new Person[] { aldiss, asimov }.AsQueryable());
            storyMock.Setup(repo => repo.Items).Returns(new Story[] { }.AsQueryable());

            var controller = new SearchController(personMock.Object, bookMock.Object, storyMock.Object);

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
            var bookMock = new Mock<IEntityRepository<Book>>();
            var personMock = new Mock<IEntityRepository<Person>>();
            var storyMock = new Mock<IEntityRepository<Story>>();

            var story1 = new Story() { Title = "The first story ever" };
            var story2 = new Story() { Title = "The very second story" };

            bookMock.Setup(repo => repo.Items).Returns(new Book[] { }.AsQueryable());
            personMock.Setup(repo => repo.Items).Returns(new Person[] { }.AsQueryable());
            storyMock.Setup(repo => repo.Items).Returns(new Story[] { story1, story2 }.AsQueryable());

            var controller = new SearchController(personMock.Object, bookMock.Object, storyMock.Object);

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
    }
}
