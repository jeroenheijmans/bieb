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

        public string Title { get; set; }
        public string SubTitle { get; set; }

        public Publisher Publisher { get; set; }

        public IList<Tag> Tags { get; set; }

        public IList<Person> Authors { get; set; }
        public IList<Person> Translators { get; set; }
    }
}
