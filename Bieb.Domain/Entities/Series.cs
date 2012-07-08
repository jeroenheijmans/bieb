using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public Series() : base()
        {
            Books = new SortedList<int, Book>();
        }

        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual SortedList<int, Book> Books { get; set; }
    }
}
