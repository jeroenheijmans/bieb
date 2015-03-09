using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public interface IIso639LanguageDisplayer
    {
        /// <summary>
        /// Get a text to "represent" a language identifier. Multiple identifiers may lead to the same representation. To get distinctly recognizable texts use <see cref="GetLocalizedIso639LanguageResourceForAdmins"/>.
        /// </summary>
        /// <param name="iso639LanguageId">ISO 639 identifier</param>
        /// <returns>A text to represent the identifier.</returns>
        string GetLocalizedIso639LanguageResource(string iso639LanguageId);

        /// <summary>
        /// Get a text that includes a link to the actual language identifier, for editing purposes. If you just want to "casually" display language use <see cref="GetLocalizedIso639LanguageResource"/> instead.
        /// </summary>
        /// <param name="iso639LanguageId">ISO 639 identifier</param>
        /// <returns>A text including the original identifier.</returns>
        string GetLocalizedIso639LanguageResourceForAdmins(string iso639LanguageId);
    }
}
