using System.Collections.Generic;

namespace Bieb.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public virtual string Name { get; set; }

        private IList<Book> _books = new List<Book>();
        public virtual IEnumerable<Book> Books
        {
            get { return _books; }
        }

        // TODO: book-originals
        // TODO: story-originals

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
