using System.Collections.Generic;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }

        private string titleSort;
        public virtual string TitleSort
        {
            get
            {
                return titleSort ?? Title;
            }
            protected internal set
            {
                titleSort = value;
            }
        }

        private readonly IDictionary<int, LibraryBook> books = new SortedList<int, LibraryBook>();
        public virtual IDictionary<int, LibraryBook> Books
        {
            get { return books; }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
