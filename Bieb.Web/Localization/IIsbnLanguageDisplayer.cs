using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Localization
{
    public interface IIsbnLanguageDisplayer
    {
        string GetLocalizedIsbnLanguageResource(int? isbnLanguageIdentifier);
    }
}
