using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public class IsbnLanguageDisplayer : IIsbnLanguageDisplayer
    {
        public string GetLocalizedIsbnLanguageResourceForAdmins(int? isbnLanguageIdentifier)
        {
            if (!isbnLanguageIdentifier.HasValue)
            {
                return BiebResources.IsbnLanguagesStrings.IsbnLanguageUnknown;
            }

            var languageText = GetLanguageText(isbnLanguageIdentifier);
            var format = BiebResources.IsbnLanguagesStrings.IsbnLanguageForAdminsFormat;

            return string.Format(format, languageText, isbnLanguageIdentifier.Value);
        }


        public string GetLocalizedIsbnLanguageResource(int? isbnLanguageIdentifier)
        {
            if (!isbnLanguageIdentifier.HasValue)
            {
                return BiebResources.IsbnLanguagesStrings.IsbnLanguageUnknown;
            }

            return GetLanguageText(isbnLanguageIdentifier);
        }


        private static string GetLanguageText(int? isbnLanguageIdentifier)
        {
            if (!isbnLanguageIdentifier.HasValue)
            {
                return BiebResources.IsbnLanguagesStrings.IsbnLanguageUnknown;
            }

            var key = "IsbnLanguage" + isbnLanguageIdentifier.Value.ToString();
            var languageText = BiebResources.IsbnLanguagesStrings.ResourceManager.GetString(key);

            return string.IsNullOrEmpty(languageText) ? BiebResources.IsbnLanguagesStrings.IsbnLanguageUnknown : languageText;
        }
    }
}