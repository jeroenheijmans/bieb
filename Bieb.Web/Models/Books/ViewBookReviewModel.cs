using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Books
{
    public class ViewBookReviewModel
    {
        public ViewBookReviewModel(Review<Book> review)
        {
            Text = review.ReviewText;
            Rating = review.Rating;
        }

        public string Text { get; set; }
        public int Rating { get; set; }
    }
}