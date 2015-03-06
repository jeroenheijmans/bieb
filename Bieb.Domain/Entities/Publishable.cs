using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Domain.Entities
{
    public abstract class Publishable : BaseEntity
    {
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


        private readonly ISet<Person> authors = new HashSet<Person>();
        public virtual ISet<Person> Authors
        {
            get { return authors; }
        }

        private readonly ISet<Person> translators = new HashSet<Person>();
        public virtual ISet<Person> Translators
        {
            get
            {
                return translators;
            }
        }

#pragma warning disable 649
        // NHibernate uses this.
        private string titleSort;
#pragma warning restore 649
        public virtual string TitleSort
        {
            get
            {
                return titleSort ?? Title;
            }
        }
    }
}
