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

        private readonly ISet<Book> authoredBooks = new HashSet<Book>();
        public virtual IEnumerable<Book> AuthoredBooks
        {
            get { return authoredBooks; }
        }

        public virtual void AddAuthoredBook(Book book)
        {
            book.Authors.Add(this);
            authoredBooks.Add(book);
        }

        private readonly ISet<Book> translatedBooks = new HashSet<Book>();
        public virtual IEnumerable<Book> TranslatedBooks
        {
            get { return translatedBooks; }
        }

        public virtual void AddTranslatedBook(Book book)
        {
            book.Translators.Add(this);
            translatedBooks.Add(book);
        }

        private readonly ISet<Book> editedBooks = new HashSet<Book>();
        public virtual IEnumerable<Book> EditedBooks
        {
            get { return editedBooks; }
        }

        public virtual void AddEditedBook(Book book)
        {
            book.Editors.Add(this);
            editedBooks.Add(book);
        }

        private readonly ISet<Story> authoredStories = new HashSet<Story>();
        public virtual IEnumerable<Story> AuthoredStories
        {
            get { return authoredStories; }
        }

        public virtual void AddAuthoredStory(Story story)
        {
            story.Authors.Add(this);
            authoredStories.Add(story);
        }
        
        private readonly ISet<Story> translatedStories = new HashSet<Story>();
        public virtual IEnumerable<Story> TranslatedStories
        {
            get { return translatedStories; }
        }

        public virtual void AddTranslatedStory(Story story)
        {
            story.Translators.Add(this);
            translatedStories.Add(story);
        }

        private readonly ISet<Review<Person>> reviews = new HashSet<Review<Person>>();
        public virtual ISet<Review<Person>> Reviews
        {
            get { return reviews; }
        }


        /// <summary>
        /// All the tags from stories in edited books, plus those from translated- and authored stories
        /// </summary>
        public virtual IEnumerable<Tag> Tags
        {
            get
            {
                return EditedBooks.SelectMany(book => book.AllTags)
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

                if (AuthoredBooks.Any() || AuthoredStories.Any()) roles.Add(Role.Author);
                if (EditedBooks.Any()) roles.Add(Role.Editor);
                if (TranslatedBooks.Any() || TranslatedStories.Any()) roles.Add(Role.Translator);

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
