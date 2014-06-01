using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class EditEntityModelMapper<TEntity, TModel> : IEditEntityModelMapper<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : EditEntityModel<TEntity>, new()
    {
        public virtual void MergeEntityWithModel(TEntity entity, TModel model)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (model.ModifiedDateTicks.HasValue)
            {
                entity.ModifiedDate = new DateTime(model.ModifiedDateTicks.Value);
            }
            else
            {
                entity.ModifiedDate = null;
            }
        }


        public virtual TModel ModelFromEntity(TEntity entity)
        {
            var model = new TModel {Id = entity.Id};

            if (entity.ModifiedDate.HasValue)
            {
                model.ModifiedDateTicks = entity.ModifiedDate.Value.Ticks;
            }

            return model;
        }
    }
}