using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class BookReviewModel : BaseDomainObjectCrudModel<Review<Book>>
    {
        protected override Review<Book> MergeWithEntitySpecifics(Review<Book> existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}