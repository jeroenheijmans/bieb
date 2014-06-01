using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class ViewEntityModelMapper<TEntity, TModel> 
        where TEntity : BaseEntity
        where TModel : ViewEntityModel<TEntity>
    {
        public abstract TModel ModelFromEntity(TEntity entity);
    }
}