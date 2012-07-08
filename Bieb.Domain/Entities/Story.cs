using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Story : BaseEntity
    {
        public Story()
            : base()
        {
            Tags = new List<Tag>();
            Authors = new List<Person>();
            Translators = new List<Person>();
        }

        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public virtual IList<Person> Authors { get; set; }
        public virtual IList<Person> Translators { get; set; }

        public override string ToString()
        {
            return Title ?? "Story (id: " + Id.ToString() + ")";
        }
    }
}
