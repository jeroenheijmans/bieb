using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Bieb.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Book() : base()
        {
            Stories = new SortedList<int, Story>();
        }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public int? ISBNLanguage { get; set; }

        /// <summary>
        /// Returns the full name of the language of the book based on the ISBNLanguage
        /// </summary>
        public string Language
        {
            get
            {
                // TODO: Localize language names.
                // TODO: Create a real conversion. 
                // - Perhaps the Internets already has a conversion list for this? 
                // - Perhaps we need to solve this in the database so it's also available for reporting?
                // - Can we use CultureInfo here?

                if (ISBNLanguage == null)
                    return "Language Unkown";

                switch (ISBNLanguage)
                {
                    case 0:
                    case 1:
                        return "English";
                    case 2:
                        return "French";
                    case 3:
                        return "German";
                    case 4:
                        return "Japanese";
                    case 5:
                        return "Russian";
                    case 90:
                        return "Dutch";
                    default:
                        return "ISBN Country Code not recognized";
                }
            }
        }

        public int? Year { get; set; }

        public Publisher Publisher { get; set; }

        /// <summary>
        /// All the Tags from all the Stories in this book
        /// </summary>
        public IEnumerable<Tag> Tags 
        { 
            get
            {
                return Stories.SelectMany(item => item.Value.Tags).Distinct();
            }
        }

        public SortedList<int, Story> Stories { get; set; }

        public IEnumerable<Person> Editors { get; set; }

        public IEnumerable<Person> Authors
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Authors).Distinct();
            }
        }

        public IEnumerable<Person> Translators
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Translators).Distinct();
            }
        }
    }
}
