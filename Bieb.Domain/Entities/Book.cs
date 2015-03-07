using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Book : Publishable
    {
        public Book()
            : base("")
        { }

        public Book(string title)
            : base(title)
        { }

        public virtual string Isbn { get; set; }

        private LibraryStatus libraryStatus = LibraryStatus.InPosession;
        public virtual LibraryStatus LibraryStatus
        {
            get { return libraryStatus; }
            set { libraryStatus = value; }
        }

        public virtual IEnumerable<Tag> AllTags
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Tags)
                    .Union(Tags)
                    .Distinct();
            }
        }

        private readonly IDictionary<int, Story> stories = new SortedList<int, Story>();
        public virtual IEnumerable<KeyValuePair<int, Story>> Stories 
        {
            get { return stories; }
        }

        public virtual void AddStory(Story story)
        {
            var index = this.stories.Any() ? this.stories.Max(s => s.Key) + 1 : 0;
            AddStory(index, story);
        }

        public virtual void AddStory(int index, Story story)
        {
            stories.Add(index, story);
            story.Book = this;
            story.PositionInBook = index;
        }

        private readonly ISet<Person> editors = new HashSet<Person>();
        public virtual IEnumerable<Person> Editors
        {
            get { return editors; }
        }

        
        // TODO: Fix this dichotomy between Series being a collection or just one thing.
        // Probably the second option is the way to go, and the first one must begone!
        private readonly IList<Series> dbSeries = new List<Series>();

        private IList<Series> DbSeries
        {
            get { return dbSeries; }
        }
        public virtual Series Series 
        { 
            get
            {
                return DbSeries == null ? null : DbSeries.FirstOrDefault();
            }
            set
            {
                DbSeries.Clear();
                DbSeries.Add(value);
            }
        }

        public virtual IEnumerable<Person> AllAuthors
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Authors)
                    .Union(Authors)
                    .Distinct();
            }
        }

        public virtual IEnumerable<Person> AllTranslators
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Translators)
                    .Union(Translators)
                    .Distinct();
            }
        }


        public override void AddAuthor(Person person)
        {
            authors.Add(person);
            person.AddAuthoredBook(this);
        }

        public override void AddTranslator(Person person)
        {
            translators.Add(person);
            person.AddTranslatedBook(this);
        }
        
        public virtual void AddEditor(Person person)
        {
            editors.Add(person);
            person.AddEditedBook(this);
        }

        public override void RemoveAuthor(Person person)
        {
            authors.Remove(person);
            person.RemoveAuthoredBook(this);
        }

        public override void RemoveTranslator(Person person)
        {
            translators.Remove(person);
            person.RemoveTranslatedBook(this);
        }

        public virtual void RemoveEditor(Person person)
        {
            editors.Remove(person);
            person.RemoveEditedBook(this);
        }

        public override void ClearAuthors()
        {
            foreach (var person in Authors)
            {
                person.RemoveAuthoredBook(this);
            }

            translators.Clear();
        }

        public override void ClearTranslators()
        {
            foreach (var person in Translators)
            {
                person.RemoveTranslatedBook(this);
            }

            translators.Clear();
        }

        public virtual void ClearEditors()
        {
            foreach (var person in Editors)
            {
                person.RemoveEditedBook(this);
            }

            editors.Clear();
        }

        public virtual BookType BookType
        {
            get
            {
                if (!Stories.Any())
                    return BookType.Novel;

                if (Stories.SelectMany(s => AllAuthors).Distinct().Count() <= 1)
                    return BookType.Collection;

                return BookType.Anthology;
            }
        }

        public virtual string ReviewText { get; set; }

        public virtual Book ReferenceBook { get; set; }

        private readonly IList<Book> referencedByBooks = new List<Book>();
        public virtual IList<Book> ReferencedByBooks
        {
            get { return referencedByBooks; }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }


    public enum BookType
    {
        /// <summary>
        /// Book with only one main story
        /// </summary>
        Novel,

        /// <summary>
        /// Book with several stories from one author
        /// </summary>
        Collection,

        /// <summary>
        /// Book with several stories from different authors
        /// </summary>
        Anthology
    }
}
