using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }

        private IDictionary<int, Book> _books = new SortedList<int, Book>();
        public virtual IDictionary<int, Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }

        public override string ToString()
        {
            return Title ?? "Series (id: " + Id.ToString() + ")";
        }
    }
}
