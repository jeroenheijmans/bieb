using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class EditEntityModel<T> where T : BaseEntity
    {
        public int Id { get; set; }

        // Ticks, because MVC looses milliseconds if it renders EditorFor DateTime properties...
        public long? ModifiedDateTicks { get; set; }
    }
}