using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditPersonReviewModel : EditEntityModel<Review<Person>>
    {
        protected override Review<Person> MergeWithEntitySpecifics(Review<Person> existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}