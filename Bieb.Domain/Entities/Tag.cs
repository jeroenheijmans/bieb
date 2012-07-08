using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
