using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class EditPersonReviewModelMapper : EditEntityModelMapper<Review<Person>, EditPersonReviewModel>
    {
        public override void MergeEntityWithModel(Review<Person> entity, EditPersonReviewModel model)
        {
            base.MergeEntityWithModel(entity, model);

            throw new NotImplementedException();
        }

        public override EditPersonReviewModel ModelFromEntity(Review<Person> entity)
        {
            var model = base.ModelFromEntity(entity);
            
            throw new NotImplementedException();

            //return model;
        }
    }
}