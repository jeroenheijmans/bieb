using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public class IsbnLanguageDisplayer : IIsbnLanguageDisplayer
    {
        // TODO: Create non-hardcoded / English implementation
        private readonly IDictionary<int, string> resources = new Dictionary<int, string>
                                                                  {
                                                                      {0, "English"},
                                                                      {1, "English"},
                                                                      {2, "French"},
                                                                      {3, "German"},
                                                                      {4, "Japanese"},
                                                                      {5, "Russian"},
                                                                      {7, "Chinese"},
                                                                      {80, "Czech"},
                                                                      {81, "Indian"},
                                                                      {82, "Norwegian"},
                                                                      {83, "Polish"},
                                                                      {84, "Spanish"},
                                                                      {85, "Brazilian"},
                                                                      {86, "Serbian"},
                                                                      {87, "Danish"},
                                                                      {88, "Italian"},
                                                                      {89, "Korean"},
                                                                      {90, "Dutch"},
                                                                      {91, "Swedish"},
                                                                      {92, "International"},
                                                                      {93, "Indian"},
                                                                      {94, "Dutch"}
                                                                  };


        public string GetLocalizedIsbnLanguageResource(int isbnLanguageIdentifier)
        {
            return string.Format("{0} ({1})", resources[isbnLanguageIdentifier], isbnLanguageIdentifier);
        }
    }
}