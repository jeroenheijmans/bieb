using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Tests.Mocks;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditBookReviewModelMapperTests
    {
        private IEntityRepository<Book> bookRepository;
        private IEditEntityModelMapper<Review<Book>, EditBookReviewModel> mapper;

        [SetUp]
        public void SetUp()
        {
            bookRepository = new BookRepositoryMock();
            mapper = new EditBookReviewModelMapper(bookRepository);
        }


        [Test]
        public void Will_Guard_Against_Null_Repository()
        {
            Assert.Throws<ArgumentNullException>(() => new EditBookReviewModelMapper(null));
        }


        [Test]
        public void Can_Set_Subject()
        {
            var review = new Review<Book> {Subject = new Book {Id = 42}};
            var model = mapper.ModelFromEntity(review);
            Assert.That(model.BookId, Is.EqualTo(review.Subject.Id));
        }


        [Test]
        public void Can_Merge_Subject_Based_On_Model_BookId()
        {
            bookRepository.Add(new Book{Id = 42});
            var model = new EditBookReviewModel {BookId = 42};
            var review = new Review<Book>();
            mapper.MergeEntityWithModel(review, model);
            Assert.That(review.Subject.Id, Is.EqualTo(42));
        }


        [Test]
        public void Will_Throw_Mapping_Exception_For_Unkown_BookId()
        {
            var model = new EditBookReviewModel {BookId = 1234};
            var review = new Review<Book>();
            Assert.Throws<MappingException>(() => mapper.MergeEntityWithModel(review, model));
        }
    }
}
