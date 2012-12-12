using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public virtual string Name { get; set; }

        private IList<LibraryBook> _libraryBooks = new List<LibraryBook>();
        public virtual IList<LibraryBook> LibraryBooks
        {
            get { return _libraryBooks; }
        }

        private IList<ReferenceBook> _referenceBooks = new List<ReferenceBook>();
        public virtual IList<ReferenceBook> ReferenceBooks
        {
            get { return _referenceBooks; }
        }

        public virtual IEnumerable<Book> Books
        {
            get
            {
                return new Book[]{}.Concat(LibraryBooks).Concat(ReferenceBooks);
            }
        }

        private IList<Story> _stories = new List<Story>();
        public virtual IEnumerable<Story> Stories
        {
            get { return _stories; }
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
