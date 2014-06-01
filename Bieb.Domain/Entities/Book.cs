using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Book : Publishable, IReviewable
    {
        public Book()
        {
            this.LibraryStatus = LibraryStatus.InPosession;
        }

        public virtual string Isbn { get; set; }

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

        private IDictionary<int, Story> _stories = new SortedList<int, Story>();
        public virtual IDictionary<int, Story> Stories 
        {
            get { return _stories; }
            set { _stories = value; }
        }

        private readonly ISet<Person> editors = new HashSet<Person>();
        public virtual ISet<Person> Editors
        {
            get { return editors; }
        }

        // Blast it. Wanted to name the collection with plural form, and the public property singular, but for "Series" there is no singular form :(. Hence the "db" prefix.
        protected virtual ICollection<Series> DbSeries { get; set; }

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
                if (Stories.Count <= 1)
                    return BookType.Novel;

                if (Stories.SelectMany(s => AllAuthors).Distinct().Count() == 1)
                    return BookType.Collection;

                return BookType.Anthology;
            }
        }

        private IList<Review<Book>> _reviews = new List<Review<Book>>();
        public virtual IList<Review<Book>> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; }
        }

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
