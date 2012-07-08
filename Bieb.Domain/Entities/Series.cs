using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        // TODO: This should be an ordered list.
        public IEnumerable<Book> Books { get; set; }
    }
}
