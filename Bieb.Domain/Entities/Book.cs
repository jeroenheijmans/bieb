using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Bieb.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Book()
            : base()
        {
            Stories = new SortedList<int, Story>();
        }

        public virtual string ISBN { get; set; }
        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual int? ISBNLanguage { get; set; }

        /// <summary>
        /// Returns the full name of the language of the book based on the ISBNLanguage
        /// </summary>
        public virtual string Language
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

        public virtual int? Year { get; set; }

        public virtual Publisher Publisher { get; set; }

        /// <summary>
        /// All the Tags from all the Stories in this book
        /// </summary>
        public virtual IEnumerable<Tag> Tags
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Tags).Distinct();
            }
        }

        public virtual SortedList<int, Story> Stories { get; set; }
        public virtual IEnumerable<Person> Editors { get; set; }

        public virtual IEnumerable<Person> Authors
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Authors).Distinct();
            }
        }

        public virtual IEnumerable<Person> Translators
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Translators).Distinct();
            }
        }
    }
}
