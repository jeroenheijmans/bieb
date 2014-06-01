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

        private readonly IDictionary<int, Book> books = new SortedList<int, Book>();
        public virtual IDictionary<int, Book> Books
        {
            get { return books; }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
