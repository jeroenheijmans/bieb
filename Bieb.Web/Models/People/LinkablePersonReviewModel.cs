using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class LinkablePersonReviewModel : LinkableEntityModel<Review<Person>>
    {
        public LinkablePersonReviewModel(Review<Person> review)
        {
            Id = review.Id;
            Text = review.ReviewText.Substring(0, 100);
        } 
    }
}