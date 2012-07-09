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

        private IList<Book> _editedBooks = new List<Book>();
        public virtual IList<Book> EditedBooks
        {
            get { return _editedBooks; }
            set { _editedBooks = value; }
        }

        private IList<Story> _authoredStories = new List<Story>();
        public virtual IList<Story> AuthoredStories
        {
            get { return _authoredStories; }
            set { _authoredStories = value; }
        }

        private IList<Story> _translatedStories = new List<Story>();
        public virtual IList<Story> TranslatedStories
        {
            get { return _translatedStories; }
            set { _translatedStories = value; }
        }

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

        public override string ToString()
        {
            return Name != null ? Name.FullName : "Person (id: " + Id.ToString() + ")";
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
