using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bieb.Tests")]

namespace Bieb.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected internal set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }

        public override string ToString()
        {
            return this.GetType().Name + " (id: " + Id.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")";
        }
    }
}
