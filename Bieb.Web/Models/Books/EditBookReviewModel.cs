using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Books
{
    public class EditBookReviewModel : EditEntityModel<Review<Book>>
    {
        public int BookId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}