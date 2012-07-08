using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Person : BaseEntity
    {
        public virtual Alias Name { get; set; }

        public virtual IEnumerable<Alias> Aliases { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual string Nationality { get; set; }

        public virtual string PlaceOfDeath { get; set; }
        public virtual string PlaceOfBirth { get; set; }

        // TODO: Implement a "UncertainDate" solution. See also: http://stackoverflow.com/questions/6431908/how-to-design-date-of-birth-in-db-and-orm-for-mix-of-known-and-unkown-dates
        public virtual string DateOfBirth { get; set; }
        public virtual string DateOfDeath { get; set; }

        public virtual IEnumerable<Book> EditedBooks { get; set; }
        public virtual IEnumerable<Story> AuthoredStories { get; set; }
        public virtual IEnumerable<Story> TranslatedStories { get; set; }

        /// <summary>
        /// All the tags from stories in edited books, plus those from translated- and authored stories
        /// </summary>
        public virtual IEnumerable<Tag> Tags
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
