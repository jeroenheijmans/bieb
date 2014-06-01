using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public abstract class Publishable : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual int? Year { get; set; }
        public virtual LibraryStatus LibraryStatus { get; set; }
        public virtual Publisher Publisher { get; set; }

        protected int? isbnLanguage;
        public virtual int? IsbnLanguage 
        {
            get { return isbnLanguage; }
            set { isbnLanguage = value; }
        }

        private string _titleSort;
        public virtual string TitleSort
        {
            get
            {
                return _titleSort ?? Title;
            }
            protected internal set
            {
                _titleSort = value;
            }
        }
    }
}
