using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Localization;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditStoryModelMapperTests
    {
        private Publisher somePublisher;
        private IEntityRepository<Publisher> publishers;
        private IEntityRepository<Person> people;
        private IBookRepository books;
        private EditStoryModelMapper mapper;
        private Person asimov, adams, wyndham;
        private Story story1, story2, story3;


        [SetUp]
        public void SetUp()
        {
            somePublisher = new Publisher { Id = 42, Name = "Penguin Books" };
            publishers = new RepositoryMock<Publisher>();
            people = new RepositoryMock<Person>();
            books = new BookRepositoryMock();

            var isbnLanguageDisplayer = new Mock<IIsbnLanguageDisplayer>();

            mapper = new EditStoryModelMapper(publishers, people, books, isbnLanguageDisplayer.Object);

            asimov = new Person { Id = 1, FirstName = "Isaac", Surname = "Asimov" };
            adams = new Person { Id = 2, FirstName = "Douglas", Surname = "Adams" };
            wyndham = new Person { Id = 3, FirstName = "John", Surname = "Wyndham" };

            story1 = new Story { Id = 1, Title = "Some short story" };
            story2 = new Story { Id = 2, Title = "Another story" };
            story3 = new Story { Id = 3, Title = "A totally different short story" };
        }


        [Test]
        public void Can_Set_Author_On_Entity()
        {
            var story = new Story();
            var model = new EditStoryModel();

            people.Add(asimov);
            model.AuthorIds = new[] {asimov.Id};
            mapper.MergeEntityWithModel(story, model);

            Assert.That(story.Authors.Single(), Is.EqualTo(asimov));
        }
    }
}
