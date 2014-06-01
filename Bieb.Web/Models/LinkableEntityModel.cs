using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public abstract class LinkableEntityModel<T> where T : BaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}