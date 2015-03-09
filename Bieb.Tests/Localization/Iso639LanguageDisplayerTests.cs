using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Localization;
using NUnit.Framework;

namespace Bieb.Tests.Localization
{
    [TestFixture]
    public class Iso639LanguageDisplayerTests
    {
        private Iso639LanguageDisplayer displayer;

        // List retrieved from http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
        // Loosely filtered based on http://en.wikipedia.org/wiki/List_of_languages_by_number_of_native_speakers
        private readonly string[] wikipediasIso639Languages = new[] { "af", "ar", "bn", "bs", "bg", "my", "ca", "zh", "hr", "cs", "da", "nl", "en", "et", "fi", "fr", "ka", "de", "el", "he", "hi", "hu", "is", "id", "ga", "it", "ja", "jv", "ko", "ku", "lv", "lt", "mk", "ms", "mr", "no", "pa", "fa", "pl", "pt", "ro", "ru", "sa", "gd", "sr", "sk", "sl", "es", "sv", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi" };
        private const string Iso639IdForEnglish = "en";


        [SetUp]
        public void SetUp()
        {
            displayer = new Iso639LanguageDisplayer();
        }


        [Test]
        public void Can_Display_English()
        {
            var result = displayer.GetLocalizedIso639LanguageResource(Iso639IdForEnglish); // English
            StringAssert.Contains("English", result);
        }


        [Test]
        public void Can_Display_English_For_Admins()
        {
            var result = displayer.GetLocalizedIso639LanguageResourceForAdmins(Iso639IdForEnglish); // English
            StringAssert.Contains("English", result);
            StringAssert.Contains(Iso639IdForEnglish.ToString(), result);
        }


        [Test]
        public void Can_Display_Unknown_Language()
        {
            var result = displayer.GetLocalizedIso639LanguageResource("");
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }


        [Test]
        public void Can_Display_Unknown_Language_For_Admins()
        {
            var result = displayer.GetLocalizedIso639LanguageResourceForAdmins("");
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }

        
        [Test]
        public void Can_Translate_Null_Key_As_Unknown_Language()
        {
            var result = displayer.GetLocalizedIso639LanguageResource(null);
            Assert.That(result, Is.Not.Null.Or.Empty);
        }


        [Test]
        public void Can_Translate_Null_Key_As_Unknown_Language_For_Admins()
        {
            var result = displayer.GetLocalizedIso639LanguageResourceForAdmins(null);
            Assert.That(result, Is.Not.Null.Or.Empty);
        }


        [Test]
        public void Can_Display_Wikipedia_Languages()
        {
            var results = wikipediasIso639Languages.Select(i => displayer.GetLocalizedIso639LanguageResource(i));
            Assert.That(results, Is.All.Not.Null.And.Not.Empty);
        }


        [Test]
        public void No_Wikipedia_Language_Will_Be_Displayed_As_Unknown()
        {
            foreach (var id in wikipediasIso639Languages)
            {
                Assert.That(displayer.GetLocalizedIso639LanguageResource(id), Is.Not.StringContaining("Unknown"), "Language {0} should not be contain string 'Unknown'.", id);
            }
        }


        [Test]
        public void Wikipedia_Language_Groups_Will_Not_Lead_To_Duplicate_Results_For_Admin_Texts()
        {
            // Some language codes are for the same language (e.g. 0 and 1 are for English, both), 
            // but the resulting texts should be distinguishable.
            var results = wikipediasIso639Languages.Select(i => displayer.GetLocalizedIso639LanguageResourceForAdmins(i));
            Assert.That(results.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key), Is.Empty, "List of duplicated languages should be empty.");
        }
    }
}
