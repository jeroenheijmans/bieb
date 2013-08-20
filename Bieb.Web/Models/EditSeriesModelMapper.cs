using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditSeriesModelMapper : EditEntityModelMapper<Series, EditSeriesModel>
    {
        public override void MergeEntityWithModel(Series entity, EditSeriesModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;
        }

        public override EditSeriesModel ModelFromEntity(Series entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;

            return model;
        }
    }
}