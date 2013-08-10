using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    /// <summary>
    /// A book that is-, has been-, or should be in my library. As opposed to books in the database that are only there for reference.
    /// </summary>
    public class LibraryBook : Book
    {
        public virtual Book ReferenceBook { get; set; }
    }
}
