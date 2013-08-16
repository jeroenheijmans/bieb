using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class BaseDomainObjectCrudModel<T> : BaseDomainObjectModel<T> where T : BaseEntity
    {
        protected BaseDomainObjectCrudModel() : base()
        { }

        protected BaseDomainObjectCrudModel(T entity) : base(entity)
        { }

        // TODO: Refactor/improve name of this method
        protected abstract T MergeWithEntitySpecifics(T existingEntity);

        // TODO: Evaluate if a ref parameter makes more sense than returning the new reference...
        public T MergeWithEntity(T existingEntity)
        {
            if (existingEntity == null) throw new ArgumentNullException("existingEntity");

            if (ModifiedDateTicks.HasValue)
            {
                existingEntity.ModifiedDate = new DateTime(ModifiedDateTicks.Value);
            }
            else
            {
                existingEntity.ModifiedDate = null;
            }

            return MergeWithEntitySpecifics(existingEntity);
        }
    }
}