using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int Version { get; set; }
    }
}
