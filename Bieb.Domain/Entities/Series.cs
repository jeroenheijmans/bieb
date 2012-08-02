using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }

        private string _titleSort;
        public virtual string TitleSort
        {
            get
            {
                return _titleSort ?? Title;
            }
            protected internal set
            {
                _titleSort = value;
            }
        }

        private IDictionary<int, Book> _books = new SortedList<int, Book>();
        public virtual IDictionary<int, Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
