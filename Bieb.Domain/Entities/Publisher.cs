using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public Publisher()
            : this("")
        { }

        public Publisher(string name)
        {
            this.Name = name;
        }

        public virtual string Name { get; set; }

        private readonly ISet<Book> books = new HashSet<Book>();
        public virtual ISet<Book> Books
        {
            get { return books; }
        }

        public virtual IEnumerable<Book> MyBooks
        {
            get { return books.Where(b => b.LibraryStatus != LibraryStatus.OnlyForReference); }
        }

        public virtual IEnumerable<Book> ReferenceBooks
        {
            get { return books.Where(b => b.LibraryStatus == LibraryStatus.OnlyForReference); }
        }

        private readonly ISet<Story> stories = new HashSet<Story>();
        public virtual ISet<Story> Stories
        {
            get { return stories; }
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
