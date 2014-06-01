using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Books
{
    public class ViewBookReviewModel : ViewEntityModel<Review<Book>>
    {
        public ViewBookReviewModel(Review<Book> review) : base(review)
        {
            Book = review.Subject.AsLinkableBookModel();
            Text = review.ReviewText;
            Rating = review.Rating;
        }

        public LinkableBookModel Book { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}