using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public virtual string Name { get; set; }
        
        public override string ToString()
        {
            return Name ?? "Publisher (id: " + Id.ToString() + ")";
        }
    }
}
