using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    class Story : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public Publisher Publisher { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Person> Authors { get; set; }
        public IEnumerable<Person> Translators { get; set; }
    }
}
