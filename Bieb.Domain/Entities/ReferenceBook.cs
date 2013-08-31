using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    /// <summary>
    /// Books that are in the database only to be "referenced", a.k.a. "originals"
    /// </summary>
    public class ReferenceBook : Book
    {
        private IList<LibraryBook> _referencedByBooks = new List<LibraryBook>();
        public virtual IList<LibraryBook> ReferencedByBooks
        {
            get { return _referencedByBooks; }
        }
    }
}
