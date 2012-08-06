using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bieb.Tests")]

namespace Bieb.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected internal set; }
        public virtual int Version { get; set; }

        public override string ToString()
        {
            return this.GetType().Name + " (id: " + Id.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")";
        }
    }
}
