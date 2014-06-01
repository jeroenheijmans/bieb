using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public interface IEditEntityModelMapper<in TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : EditEntityModel<TEntity>, new()
    {
        void MergeEntityWithModel(TEntity entity, TModel model);
        TModel ModelFromEntity(TEntity entity);
    }
}