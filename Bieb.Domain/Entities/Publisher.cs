using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public virtual string Name { get; set; }

        private readonly ISet<LibraryBook> libraryBooks = new HashSet<LibraryBook>();
        public virtual ISet<LibraryBook> LibraryBooks
        {
            get { return libraryBooks; }
        }

        private readonly ISet<ReferenceBook> referenceBooks = new HashSet<ReferenceBook>();
        public virtual ISet<ReferenceBook> ReferenceBooks
        {
            get { return referenceBooks; }
        }

        public virtual IEnumerable<Book> Books
        {
            get
            {
                return new Book[]{}.Concat(LibraryBooks).Concat(ReferenceBooks);
            }
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
