using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Localization;
using Bieb.Web.Models;
using NUnit.Framework;

namespace Bieb.Tests.Models
{
    [TestFixture]
    public class IsbnLanguageDisplayerTests
    {
        private IsbnLanguageDisplayer displayer;

        // List retrieved from http://en.wikipedia.org/wiki/List_of_ISBN_identifier_groups
        // Taken only the 1 and 2 digit identifiers
        private readonly int[] wikipediasIsbnLanguages = new[] { 0, 1, 2, 3, 4, 5, 7, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94 };
        private const int isbnIdForEnglish = 1;


        [SetUp]
        public void SetUp()
        {
            displayer = new IsbnLanguageDisplayer();
        }


        [Test]
        public void Can_Display_English()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(isbnIdForEnglish); // English
            StringAssert.Contains("English", result);
        }


        [Test]
        public void Can_Display_English_For_Admins()
        {
            var result = displayer.GetLocalizedIsbnLanguageResourceForAdmins(isbnIdForEnglish); // English
            StringAssert.Contains("English", result);
            StringAssert.Contains(isbnIdForEnglish.ToString(), result);
        }


        [Test]
        public void Can_Display_Unkown_Language()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(-1);
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }


        [Test]
        public void Can_Display_Unkown_Language_For_Admins()
        {
            var result = displayer.GetLocalizedIsbnLanguageResourceForAdmins(-1);
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }

        
        [Test]
        public void Can_Translate_Null_Key_As_Unkown_Language()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(null);
            Assert.That(result, Is.Not.Null.Or.Empty);
        }


        [Test]
        public void Can_Translate_Null_Key_As_Unkown_Language_For_Admins()
        {
            var result = displayer.GetLocalizedIsbnLanguageResourceForAdmins(null);
            Assert.That(result, Is.Not.Null.Or.Empty);
        }


        [Test]
        public void Can_Display_Wikipedia_Languages()
        {
            var results = wikipediasIsbnLanguages.Select(i => displayer.GetLocalizedIsbnLanguageResource(i));
            Assert.That(results, Is.All.Not.Null.And.Not.Empty);
        }


        [Test]
        public void No_Wikipedia_Language_Will_Be_Displayed_As_Unknown()
        {
            foreach (var id in wikipediasIsbnLanguages)
            {
                Assert.That(displayer.GetLocalizedIsbnLanguageResource(id), Is.Not.StringContaining("Unknown"), "Language {0} should not be contain string 'Unknown'.", id);
            }
        }


        [Test]
        public void Wikipedia_Language_Groups_Will_Not_Lead_To_Duplicate_Results_For_Admin_Texts()
        {
            // Some language codes are for the same language (e.g. 0 and 1 are for English, both), 
            // but the resulting texts should be distinguishable.
            var results = wikipediasIsbnLanguages.Select(i => displayer.GetLocalizedIsbnLanguageResourceForAdmins(i));
            Assert.That(results.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key), Is.Empty, "List of duplicated languages should be empty.");
        }
    }
}
