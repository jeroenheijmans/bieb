using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;

namespace Bieb.Domain.Repositories
{
    public interface IBookRepository : IEntityRepository<Book>
    {
        /// <summary>
        /// Returns ISO 639 two-letter identifiers representing the available Languages.
        /// </summary>
        IEnumerable<string> Iso639LanguageIdentifiers { get; }
    }
}
