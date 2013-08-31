using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual string FullNameAlphabetical
        {
            get
            {
                var secondPart = (string.IsNullOrEmpty(Title) ? "" : Title + " ")
                                 +
                                 (string.IsNullOrEmpty(FirstName) ? "" : FirstName + " ")
                                 +
                                 Prefix;

                if (!string.IsNullOrEmpty(secondPart))
                {
                    return Surname +  ", " + secondPart.Trim();
                }

                return Surname;
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
            set
            {
                DateOfBirthFrom = value.FromDate;
                DateOfBirthUntil = value.UntilDate;
            }
        }

        public virtual UncertainDate DateOfDeath
        {
            get
            {
                return new UncertainDate(DateOfDeathFrom, DateOfDeathUntil);
            }
            set
            {
                DateOfDeathFrom = value.FromDate;
                DateOfDeathUntil = value.UntilDate;
            }
        }

        private readonly ISet<LibraryBook> editedBooks = new HashSet<LibraryBook>();
        public virtual ISet<LibraryBook> EditedBooks
        {
            get { return editedBooks; }
        }

        private readonly ISet<Story> authoredStories = new HashSet<Story>();
        public virtual ISet<Story> AuthoredStories
        {
            get { return authoredStories; }
        }

        public virtual IEnumerable<Story> AuthoredShortStories
        {
            get
            {
                return authoredStories
                        .Where(s => s.Book != null && s.Book.BookType != BookType.Novel)
                        .Select(s => s);
            }
        }

        public virtual IEnumerable<LibraryBook> AuthoredNovels
        {
            get
            {
                return authoredStories
                        .Select(s => s.Book)
                        .Where(b => b != null && b.BookType == BookType.Novel)
                        .Distinct();
            }
        }

        private readonly ISet<Story> translatedStories = new HashSet<Story>();
        public virtual ISet<Story> TranslatedStories
        {
            get { return translatedStories; }
        }

        public virtual IEnumerable<Story> TranslatedShortStories
        {
            get
            {
                return translatedStories
                        .Where(s => s.Book.BookType == BookType.Anthology || s.Book.BookType == BookType.Collection);
            }
        }

        public virtual IEnumerable<LibraryBook> TranslatedNovels
        {
            get
            {
                return translatedStories
                        .Select(s => s.Book)
                        .Where(b => b != null && b.BookType == BookType.Novel)
                        .Distinct();
            }
        }

        private readonly ISet<Review<Person>> reviews = new HashSet<Review<Person>>();
        public virtual ISet<Review<Person>> Reviews
        {
            get { return reviews; }
        }

        public virtual IEnumerable<LibraryBook> AuthoredCollections
        {
            get 
            {
                return authoredStories
                        .Select(s => s.Book)
                        .Where(b => b != null && b.BookType == BookType.Collection)
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
