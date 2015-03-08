using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Series : BaseEntity
    {
        public Series()
            : this("")
        { }

        public Series(string title)
        {
            this.Title = title;
        }

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


        public virtual void AddBook(Book book)
        {
            var index = this.books.Any() ? this.books.Max(b => b.Key) + 1 : 0;
            this.books.Add(index, book);
            book.Series = this;
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
