using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class BaseDomainObjectModel<T> where T : BaseEntity
    {
        protected BaseDomainObjectModel()
        { }

        protected BaseDomainObjectModel(T entity)
        {
            this.Id = entity.Id;

            if (entity.ModifiedDate.HasValue)
            {
                this.ModifiedDateTicks = entity.ModifiedDate.Value.Ticks;
            }
        }

        public int Id { get; set; }
        
        // Ticks, because MVC looses milliseconds if it renders EditorFor DateTime properties...
        public long? ModifiedDateTicks { get; set; }
    }
}