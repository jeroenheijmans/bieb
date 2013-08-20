using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditPublisherModelMapper : EditEntityModelMapper<Publisher, EditPublisherModel>
    {
        public override void MergeEntityWithModel(Publisher entity, EditPublisherModel model)
        {
            base.MergeEntityWithModel(entity, model);
            entity.Name = model.Name;
        }

        public override EditPublisherModel ModelFromEntity(Publisher entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Name = entity.Name;

            return model;
        }
    }
}