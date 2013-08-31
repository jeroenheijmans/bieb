using System.Linq;
using System.Web.Mvc;
using Bieb.Tests.Mocks;
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
        private IEntityRepository<LibraryBook> bookRepository;
        private IEntityRepository<Person> peopleRepository;
        private IEntityRepository<Story> storyRepository;
        private SearchController controller;


        [SetUp]
        public void SetUp()
        {
            peopleRepository = new RepositoryMock<Person>();
            bookRepository = new RepositoryMock<LibraryBook>();
            storyRepository = new RepositoryMock<Story>();

            controller = new SearchController(peopleRepository, bookRepository, storyRepository);
        }


        [Test]
        public void Can_Find_Book_With_Basic_Search()
        {
            var markTheMartian = new LibraryBook { Title = "Mark the Martian" };
            var markmanship = new LibraryBook { Title = "Markmanship" };
            var martinTheEarthling = new LibraryBook { Title = "Martin the Earthling" };

            bookRepository.Add(markTheMartian);
            bookRepository.Add(martinTheEarthling);
            bookRepository.Add(markmanship);

            var result = (ViewResult)controller.Basic("Mark");
            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.books.Count(), Is.EqualTo(2));
            Assert.That(searchResults.books, Contains.Item(markTheMartian));
            Assert.That(searchResults.books, Contains.Item(markmanship));
        }


        [Test]
        public void Can_Find_Person_With_Basic_Search()
        {
            var aldiss = new Person { FirstName = "Brian", Surname = "Aldiss" };
            var asimov = new Person { FirstName = "Isaac", Surname = "Asimov" };

            peopleRepository.Add(aldiss);
            peopleRepository.Add(asimov);

            var result = (ViewResult)controller.Basic("is");

            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.people.Count(), Is.EqualTo(2));
            Assert.That(searchResults.people, Contains.Item(aldiss));
            Assert.That(searchResults.people, Contains.Item(asimov));
        }


        [Test]
        public void Can_Find_Story_With_Basic_Search()
        {
            var bundle = new LibraryBook();
            var story1 = new Story { Title = "The first story ever", Book = bundle };
            var story2 = new Story { Title = "The very second story", Book = bundle };
            var story3 = new Story { Title = "The third ever story", Book = bundle };
            bundle.Stories.Add(0, story1);
            bundle.Stories.Add(1, story2);

            storyRepository.Add(story1);
            storyRepository.Add(story2);
            storyRepository.Add(story3);


            var result = (ViewResult)controller.Basic("ever");
            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.stories.Count(), Is.EqualTo(2));
            Assert.That(searchResults.stories, Contains.Item(story1));
            Assert.That(searchResults.stories, Contains.Item(story3));
        }


        [Test]
        public void Will_Not_Show_Stories_From_Novels_With_Basic_Search()
        {
            var novel = new LibraryBook { };
            var story = new Story { Title = "Best book ever", Book = novel };
            novel.Stories.Add(0, story);

            storyRepository.Add(story);

            var result = (ViewResult)controller.Basic("ever");
            
            var searchResults = (BasicSearchResultModel)result.Model;
            Assert.That(searchResults.stories.Count(), Is.EqualTo(0));
        }


        [Test]
        public void Single_Person_Result_Will_Redirect_To_Appropriate_Details_Page()
        {
            var asimov = new Person { FirstName = "Isaac", Surname = "Asimov" };

            peopleRepository.Add(asimov);

            var result = (RedirectToRouteResult)controller.Basic("asimov");

            Assert.That(result.RouteValues["controller"], Is.EqualTo("People"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Single_Book_Result_Will_Redirect_To_Appropriate_Details_Page()
        {
            var markTheMartian = new LibraryBook { Title = "Mark the Martian" };

            bookRepository.Add(markTheMartian);

            var result = (RedirectToRouteResult)controller.Basic("Mark");

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Single_Story_Result_Will_Redirect_To_Appropriate_Details_Page()
        {
            var bundle = new LibraryBook();
            var story1 = new Story { Title = "The first story ever", Book = bundle };
            var story2 = new Story { Title = "The very second story", Book = bundle };
            bundle.Stories.Add(0, story1);
            bundle.Stories.Add(1, story2);

            storyRepository.Add(story1);
            storyRepository.Add(story2);

            var result = (RedirectToRouteResult)controller.Basic("ever");

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
        }
    }
}
