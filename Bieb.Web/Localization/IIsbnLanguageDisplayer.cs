using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public interface IIsbnLanguageDisplayer
    {
        /// <summary>
        /// Get a text to "represent" a language identifier. Multiple identifiers may lead to the same representation. To get distinctly recognizable texts use <see cref="GetLocalizedIsbnLanguageResourceForAdmins"/>.
        /// </summary>
        /// <param name="isbnLanguageIdentifier">ISBN identifier</param>
        /// <returns>A text to represent the identifier.</returns>
        string GetLocalizedIsbnLanguageResource(int? isbnLanguageIdentifier);

        /// <summary>
        /// Get a text that includes a link to the actual language identifier, for editing purposes. If you just want to "casually" display language use <see cref="GetLocalizedIsbnLanguageResource"/> instead.
        /// </summary>
        /// <param name="isbnLanguageIdentifier">ISBN identifier</param>
        /// <returns>A text including the original identifier.</returns>
        string GetLocalizedIsbnLanguageResourceForAdmins(int? isbnLanguageIdentifier);
    }
}
