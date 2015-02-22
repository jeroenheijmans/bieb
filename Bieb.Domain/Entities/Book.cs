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

        private readonly ISet<Tag> tags = new HashSet<Tag>();
        public virtual ISet<Tag> Tags
        {
            get { return tags; }
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

        private IDictionary<int, Story> stories = new SortedList<int, Story>();
        public virtual IEnumerable<KeyValuePair<int, Story>> Stories 
        {
            get { return stories; }
        }

        public virtual void AddStory(Story story)
        {
            stories.Add(0, story);
            story.Book = this;
            story.PositionInBook = 0;
        }

        public virtual void AddStory(int index, Story story)
        {
            stories.Add(index, story);
            story.Book = this;
            story.PositionInBook = index;
        }

        private readonly ISet<Person> editors = new HashSet<Person>();
        public virtual ISet<Person> Editors
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

        private readonly ISet<Person> authors = new HashSet<Person>();
        public virtual ISet<Person> Authors
        {
            get { return authors; }
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

        private readonly ISet<Person> translators = new HashSet<Person>();
        public virtual ISet<Person> Translators
        {
            get 
            {
                return translators; 
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

        public virtual BookType BookType
        {
            get
            {
                if (Stories.Count() <= 1)
                    return BookType.Novel;

                if (Stories.SelectMany(s => AllAuthors).Distinct().Count() == 1)
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
