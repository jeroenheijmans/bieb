using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    class Person : BaseEntity
    {
        public Alias Name { get; set; }

        public IEnumerable<Alias> Aliases { get; set; }
        
        public Gender Gender { get; set; }

        public string Nationality { get; set; }

        public string PlaceOfDeath { get; set; }
        public string PlaceOfBirth { get; set; }

        // TODO: Implement a "UncertainDate" solution. See also: http://stackoverflow.com/questions/6431908/how-to-design-date-of-birth-in-db-and-orm-for-mix-of-known-and-unkown-dates
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }

        public IEnumerable<Book> EditedBooks { get; set; }

        public IEnumerable<Story> AuthoredStories { get; set; }
        public IEnumerable<Story> TranslatedStories { get; set; }

        /// <summary>
        /// All the tags from stories in edited books, plus those from translated- and authored stories
        /// </summary>
        public IEnumerable<Tag> Tags
        {
            get
            {
                return EditedBooks.SelectMany(book => book.Tags)
                        .Union(
                            AuthoredStories.SelectMany(story => story.Tags)
                        )
                        .Union(
                            TranslatedStories.SelectMany(story => story.Tags)
                        );
            }
        }
    }
    
    public enum Gender
    {
        Male = 'M',
        Female = 'F',
        Unkown = '?',

        /// <summary>
        /// For example when one Author "Person" is in fact many different people
        /// </summary>
        None = '-'        
    }
}
