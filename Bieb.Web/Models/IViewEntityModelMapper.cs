using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    // Covariance/contravariance with "in" and "out" based on Resharper's suggestion, explained here http://stackoverflow.com/a/8317223/419956
    // Seems like a good fit for this interface.
    public interface IViewEntityModelMapper<in TEntity, out TModel> 
        where TEntity : BaseEntity
        where TModel : ViewEntityModel<TEntity>
    {
        TModel ModelFromEntity(TEntity entity);
    }
}