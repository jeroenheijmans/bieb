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

        private readonly ISet<Tag> bookTags = new HashSet<Tag>(); 
        public virtual ISet<Tag> BookTags
        {
            get { return bookTags; }
        } 

        public virtual IEnumerable<Tag> AllTags
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Tags)
                    .Union(BookTags)
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
        protected virtual ICollection<Series> DBSeries { get; set; }

        public virtual Series Series 
        { 
            get
            {
                return DBSeries.FirstOrDefault();
            }
            set
            {
                DBSeries.Clear();
                DBSeries.Add(value);
            }
        }

        private readonly ISet<Person> bookAuthors = new HashSet<Person>(); 
        public virtual ISet<Person> BookAuthors
        {
            get { return bookAuthors; }
        }

        public virtual IEnumerable<Person> AllAuthors
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Authors)
                    .Union(BookAuthors)
                    .Distinct();
            }
        }

        private readonly ISet<Person> bookTranslators = new HashSet<Person>();
        public virtual ISet<Person> BookTranslators
        {
            get 
            {
                return bookTranslators; 
            }
        }

        public virtual IEnumerable<Person> AllTranslators
        {
            get
            {
                return Stories
                    .SelectMany(item => item.Value.Translators)
                    .Union(BookTranslators)
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
