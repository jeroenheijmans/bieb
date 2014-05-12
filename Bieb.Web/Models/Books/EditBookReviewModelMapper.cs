using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditBookReviewModelMapper : EditEntityModelMapper<Review<Book>, EditBookReviewModel>
    {
        public override void MergeEntityWithModel(Review<Book> entity, EditBookReviewModel model)
        {
            base.MergeEntityWithModel(entity, model);

            throw new NotImplementedException();
        }

        public override EditBookReviewModel ModelFromEntity(Review<Book> entity)
        {
            var model = base.ModelFromEntity(entity);

            throw new NotImplementedException();
        }
    }
}