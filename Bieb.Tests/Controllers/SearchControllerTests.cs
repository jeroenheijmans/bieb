using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Bieb.Tests.Mocks;
using Bieb.Web.Models.Search;
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
        private IEntityRepository<Book> bookRepository;
        private IEntityRepository<Person> peopleRepository;
        private IEntityRepository<Story> storyRepository;
        private SearchController controller;


        [SetUp]
        public void SetUp()
        {
            peopleRepository = new RepositoryMock<Person>();
            bookRepository = new RepositoryMock<Book>();
            storyRepository = new RepositoryMock<Story>();

            controller = new SearchController(peopleRepository, bookRepository, storyRepository);
        }


        [Test]
        public void Can_Find_Book_With_Basic_Search()
        {
            var markTheMartian = new Book { Title = "Mark the Martian" };
            var markmanship = new Book { Title = "Markmanship" };
            var martinTheEarthling = new Book { Title = "Martin the Earthling" };

            bookRepository.Add(markTheMartian);
            bookRepository.Add(martinTheEarthling);
            bookRepository.Add(markmanship);

            var result = (ViewResult)controller.Basic("Mark");
            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.Books.Count(), Is.EqualTo(2));
            Assert.That(searchResults.Books.Count(b => b.Text == markTheMartian.Title), Is.EqualTo(1), "Can find markTheMartian");
            Assert.That(searchResults.Books.Count(b => b.Text == markmanship.Title), Is.EqualTo(1), "Can find markTheMartian");
        }


        [Test]
        public void Can_Find_Person_With_Basic_Search()
        {
            var aldiss = new Person { Id = 1, FirstName = "Brian", Surname = "Aldiss" };
            var asimov = new Person { Id = 2, FirstName = "Isaac", Surname = "Asimov" };

            peopleRepository.Add(aldiss);
            peopleRepository.Add(asimov);

            var result = (ViewResult)controller.Basic("is");

            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.People.Count(), Is.EqualTo(2));
            Assert.That(searchResults.People.Count(p => p.Id == aldiss.Id), Is.EqualTo(1), "Can find Aldiss");
            Assert.That(searchResults.People.Count(p => p.Id == asimov.Id), Is.EqualTo(1), "Can find Asimov");
        }


        [Test]
        public void Can_Find_Story_With_Basic_Search()
        {
            var bundle = new Book();
            var story1 = new Story { Id = 1, Title = "The first story ever", Book = bundle };
            var story2 = new Story { Id = 2, Title = "The very second story", Book = bundle };
            var story3 = new Story { Id = 3, Title = "The third ever story", Book = bundle };
            bundle.AddStory(0, story1);
            bundle.AddStory(1, story2);

            storyRepository.Add(story1);
            storyRepository.Add(story2);
            storyRepository.Add(story3);
            
            var result = (ViewResult)controller.Basic("ever");
            var searchResults = (BasicSearchResultModel)result.Model;

            Assert.That(searchResults.Stories.Count(), Is.EqualTo(2));
            Assert.That(searchResults.Stories.Count(s => s.Id == story1.Id), Is.EqualTo(1), "Found story 1");
            Assert.That(searchResults.Stories.Count(s => s.Id == story3.Id), Is.EqualTo(1), "Found story 3");
        }


        [Test]
        public void Will_Not_Show_Stories_From_Novels_With_Basic_Search()
        {
            var novel = new Book { };
            var story = new Story { Title = "Best book ever", Book = novel };
            novel.AddStory(0, story);

            storyRepository.Add(story);

            var result = (ViewResult)controller.Basic("ever");
            
            var searchResults = (BasicSearchResultModel)result.Model;
            Assert.That(searchResults.Stories.Count(), Is.EqualTo(0));
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
            var markTheMartian = new Book { Title = "Mark the Martian" };

            bookRepository.Add(markTheMartian);

            var result = (RedirectToRouteResult)controller.Basic("Mark");

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Single_Story_Result_Will_Redirect_To_Appropriate_Details_Page()
        {
            var bundle = new Book();
            var story1 = new Story { Title = "The first story ever", Book = bundle };
            var story2 = new Story { Title = "The very second story", Book = bundle };
            bundle.AddStory(0, story1);
            bundle.AddStory(1, story2);

            storyRepository.Add(story1);
            storyRepository.Add(story2);

            var result = (RedirectToRouteResult)controller.Basic("ever");

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Books"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
        }


        [Test]
        public void Can_Handle_Null_Query()
        {
            var result = controller.Basic(null) as ViewResult;
            Assert.That(result.Model, Is.InstanceOf<BasicSearchResultModel>());
        }
    }
}
