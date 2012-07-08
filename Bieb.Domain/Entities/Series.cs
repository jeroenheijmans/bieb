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

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public SortedList<int, Book> Books { get; set; }
    }
}
