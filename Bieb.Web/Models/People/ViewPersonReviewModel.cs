using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class ViewPersonReviewModel : ViewEntityModel<Review<Person>>
    {
        public ViewPersonReviewModel(Review<Person> entity) : base(entity)
        {
            Person = new LinkablePersonModel(entity.Subject);
            ReviewText = entity.ReviewText;
        }

        public LinkablePersonModel Person { get; set; }
        public string ReviewText { get; set; }
    }
}