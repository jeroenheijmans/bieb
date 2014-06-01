using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class ViewEntityModel<T> where T : BaseEntity
    {
        protected ViewEntityModel(T entity)
        {
            Id = entity.Id;
        }

        public int Id { get; set; }
    }
}