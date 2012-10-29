using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.CustomDataTypes;

namespace Bieb.Domain.Entities
{
    public class Person : BaseEntity, IReviewable
    {
        public virtual string Title { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Prefix { get; set; }
        public virtual string Surname { get; set; }

        public virtual string FullName
        {
            get
            {
                return (
                        (string.IsNullOrWhiteSpace(Title) ? "" : Title + " ")
                        +
                        (string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName + " ")
                        +
                        (string.IsNullOrWhiteSpace(Prefix) ? "" : Prefix + " ")
                        +
                        (string.IsNullOrWhiteSpace(Surname) ? "" : Surname)
                    ).Trim();
            }
        }
        
        public virtual Gender Gender { get; set; }

        public virtual char GenderChar 
        { 
            get
            {
                switch (Gender)
                {
                    case Gender.None:
                        return '-';
                    case Gender.Male:
                        return '♂';
                    case Gender.Female:
                        return '♀';
                    case Gender.Unkown:
                    default:
                        return '?';
                }
            }
        }

        public virtual string Nationality { get; set; }

        public virtual string PlaceOfDeath { get; set; }
        public virtual string PlaceOfBirth { get; set; }

        public virtual DateTime? DateOfBirthFrom { get; set; }
        public virtual DateTime? DateOfBirthUntil { get; set; }
        public virtual DateTime? DateOfDeathFrom { get; set; }
        public virtual DateTime? DateOfDeathUntil { get; set; }

        public virtual UncertainDate DateOfBirth 
        { 
            get
            {
                return new UncertainDate(DateOfBirthFrom, DateOfBirthUntil);
            }

            // TODO: A setter would be nice, propagating to the until/from dates
        }

        public virtual UncertainDate DateOfDeath
        {
            get
            {
                return new UncertainDate(DateOfDeathFrom, DateOfDeathUntil);
            }

            // TODO: A setter would be nice, propagating to the until/from dates
        }

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

        public virtual IEnumerable<Story> AuthoredShortStories
        {
            get
            {
                return _authoredStories
                        .Where(s => s.Book.BookType != BookType.Novel)
                        .Select(s => s);
            }
        }

        public virtual IEnumerable<Book> AuthoredNovels
        {
            get
            {
                return _authoredStories
                        .Where(s => s.Book.BookType == BookType.Novel)
                        .Select(s => s.Book);
            }
        }

        private IList<Story> _translatedStories = new List<Story>();
        public virtual IList<Story> TranslatedStories
        {
            get { return _translatedStories; }
            set { _translatedStories = value; }
        }

        private IList<Review<Person>> _reviews = new List<Review<Person>>();
        public virtual IList<Review<Person>> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; }
        }

        public virtual IEnumerable<Book> AuthoredCollections
        {
            get 
            {
                return _authoredStories
                        .Select(s => s.Book)
                        .Where(b => b.BookType == BookType.Collection)
                        .Distinct();
            }
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

        public virtual IEnumerable<Role> Roles
        {
            get
            {
                var roles = new List<Role>();

                if (AuthoredStories.Count > 0) roles.Add(Role.Author);
                if (EditedBooks.Count > 0) roles.Add(Role.Editor);
                if (TranslatedStories.Count > 0) roles.Add(Role.Translator);

                return roles;
            }
        }

        public override string ToString()
        {
            return FullName ?? base.ToString();
        }
    }
    
    public enum Gender
    {
        Unkown = 0,
        Male = 1,
        Female = 2,

        /// <summary>
        /// For example when one Author "Person" is in fact many different people
        /// </summary>
        None = 3
    }

    public enum Role
    {
        Author,
        Translator,
        Editor
    }
}
