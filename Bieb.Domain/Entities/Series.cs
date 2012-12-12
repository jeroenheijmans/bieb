using System.Collections.Generic;

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

        private IDictionary<int, LibraryBook> _books = new SortedList<int, LibraryBook>();
        public virtual IDictionary<int, LibraryBook> Books
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
