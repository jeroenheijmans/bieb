using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public abstract class Publishable : BaseEntity
    {
        protected Publishable()
        { }

        protected Publishable(string title)
        {
            this.title = title;
        }

        private string title;

        public virtual string Title
        {
            get { return title; }
            set { title = value; }
        }

        public virtual string Subtitle { get; set; }
        public virtual int? Year { get; set; }

        public virtual Publisher Publisher { get; set; }

        protected int? isbnLanguage;
        public virtual int? IsbnLanguage 
        {
            get { return isbnLanguage; }
            set { isbnLanguage = value; }
        }

        private string titleSort;
        public virtual string TitleSort
        {
            get
            {
                return titleSort ?? Title;
            }
        }
    }
}
