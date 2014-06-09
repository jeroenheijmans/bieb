using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Models.Books
{
    public class EditBookReviewModelMapper : EditEntityModelMapper<Review<Book>, EditBookReviewModel>
    {
        private readonly IEntityRepository<Book> books;

        public EditBookReviewModelMapper(IEntityRepository<Book> books)
        {
            if (books == null)
            {
                throw new ArgumentNullException("books");
            }

            this.books = books;
        }

        public override void MergeEntityWithModel(Review<Book> entity, EditBookReviewModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Rating = model.Rating;
            entity.ReviewText = model.Text;

            var book = books.GetItem(model.BookId);

            if (book == null)
            {
                throw new MappingException("Provided BookId for review wasn't for any known book.");
            }

            entity.Subject = book;
        }

        public override EditBookReviewModel ModelFromEntity(Review<Book> entity)
        {
            var model = base.ModelFromEntity(entity);

            model.BookId = entity.Subject.Id;
            model.Text = entity.ReviewText;
            model.Rating = entity.Rating;

            return model;
        }
    }
}