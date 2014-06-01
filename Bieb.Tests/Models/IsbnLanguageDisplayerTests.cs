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

        [SetUp]
        public void SetUp()
        {
            displayer = new IsbnLanguageDisplayer();
        }

        [Test]
        public void Can_Display_English()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(1); // English
            Assert.That(result, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Will_Throw_Exception_For_NonExistent_Lanuage_Identifier()
        {
            Assert.Throws<KeyNotFoundException>(() => displayer.GetLocalizedIsbnLanguageResource(-1));
        }

        [Test]
        public void Can_Display_All_Main_Languages_From_Wikipedia()
        {
            // List retrieved from http://en.wikipedia.org/wiki/List_of_ISBN_identifier_groups
            // Taken only the 1 and 2 digit identifiers
            var wikipediasIsbnLanguages = new[]
                                              {
                                                  0, 1, 2, 3, 4, 5, 7, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94
                                              };

            foreach (var key in wikipediasIsbnLanguages)
            {
                int keyForTestDelegateClosure = key;
                Assert.DoesNotThrow(() => displayer.GetLocalizedIsbnLanguageResource(keyForTestDelegateClosure), "Key {0} could not be displayed.", key);
            }
        }

        [Test]
        public void Will_Include_LanguageId_In_Resource()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(90);
            Assert.That(result.Contains("90"));
        }

        [Test]
        public void Can_Translate_Null_Key_As_Unkown_Language()
        {
            var result = displayer.GetLocalizedIsbnLanguageResource(null);
            Assert.That(result, Is.Not.Null.Or.Empty);
        }
    }
}
