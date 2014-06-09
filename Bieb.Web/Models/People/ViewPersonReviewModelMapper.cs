using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class ViewPersonReviewModelMapper : IViewEntityModelMapper<Review<Person>, ViewPersonReviewModel>
    {
        public ViewPersonReviewModel ModelFromEntity(Review<Person> entity)
        {
            return new ViewPersonReviewModel(entity);
        }
    }
}