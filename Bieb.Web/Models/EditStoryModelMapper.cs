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

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
        }

        public override EditStoryModel ModelFromEntity(Story entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;

            return model;
        }
    }
}