using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public class Book : Publishable, IReviewable
    {
        public virtual string Isbn { get; set; }

        /// <summary>
        /// All the Tags from all the Stories in this book
        /// </summary>
        public virtual IEnumerable<Tag> Tags
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Tags).Distinct();
            }
        }

        private IDictionary<int, Story> _stories = new SortedList<int, Story>();
        public virtual IDictionary<int, Story> Stories 
        {
            get { return _stories; }
            set { _stories = value; }
        }

        private IList<Person> _editors = new List<Person>();
        public virtual IList<Person> Editors
        {
            get { return _editors; }
            set { _editors = value; }
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

        public virtual IEnumerable<Person> Authors
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Authors).Distinct();
            }
        }

        public virtual IEnumerable<Person> Translators
        {
            get
            {
                return Stories.SelectMany(item => item.Value.Translators).Distinct();
            }
        }

        public virtual BookType BookType
        {
            get
            {
                if (Stories.Count <= 1)
                    return BookType.Novel;

                if (Stories.SelectMany(s => Authors).Distinct().Count() == 1)
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
