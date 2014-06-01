using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Books
{
    public class ViewBookReviewModelMapper : IViewEntityModelMapper<Review<Book>, ViewBookReviewModel>
    {
        public ViewBookReviewModel ModelFromEntity(Review<Book> entity)
        {
            return new ViewBookReviewModel(entity);
        }
    }
}