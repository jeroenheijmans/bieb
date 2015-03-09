using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public class Iso639LanguageDisplayer : IIso639LanguageDisplayer
    {
        public string GetLocalizedIso639LanguageResourceForAdmins(string iso639LanguageIdentifier)
        {
            if (iso639LanguageIdentifier == null)
            {
                return BiebResources.Iso639LanguagesStrings.Iso639LanguageUnknown;
            }

            var languageText = GetLanguageText(iso639LanguageIdentifier);
            var format = BiebResources.Iso639LanguagesStrings.Iso639LanguageForAdminsFormat;

            return string.Format(format, languageText, iso639LanguageIdentifier);
        }


        public string GetLocalizedIso639LanguageResource(string iso639LanguageIdentifier)
        {
            if (iso639LanguageIdentifier == null)
            {
                return BiebResources.Iso639LanguagesStrings.Iso639LanguageUnknown;
            }

            return GetLanguageText(iso639LanguageIdentifier);
        }


        private static string GetLanguageText(string iso639LanguageIdentifier)
        {
            if (iso639LanguageIdentifier == null)
            {
                return BiebResources.Iso639LanguagesStrings.Iso639LanguageUnknown;
            }

            var key = "Iso639Language_" + iso639LanguageIdentifier;
            var languageText = BiebResources.Iso639LanguagesStrings.ResourceManager.GetString(key);

            return string.IsNullOrEmpty(languageText) ? BiebResources.Iso639LanguagesStrings.Iso639LanguageUnknown : languageText;
        }
    }
}