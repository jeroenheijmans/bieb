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

        
        // TODO: Research / improve these (naive? old?) implementations taken from Ayende's blog

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity;

            if (other == null)
                return false;

            var otherIsTransient = Equals(other.Id, default(int));
            var thisIsTransient = Equals(this.Id, default(int));

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }

        private int? _oldHashCode;

        public override int GetHashCode()
        {
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;

            bool thisIsTransient = Equals(this.Id, default(int));

            if (thisIsTransient)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}
