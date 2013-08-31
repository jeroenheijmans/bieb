using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditStoryModelMapper : EditEntityModelMapper<Story, EditStoryModel>
    {
        public override void MergeEntityWithModel(Story entity, EditStoryModel model)
        {
            base.MergeEntityWithModel(entity, model);

            throw new NotImplementedException();
        }

        public override EditStoryModel ModelFromEntity(Story entity)
        {
            var model = base.ModelFromEntity(entity);

            throw new NotImplementedException();

            //return model;
        }
    }
}