using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Story : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Book Book { get; set; }
        public virtual int PositionInBook { get; set; }

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

        private IList<Tag> _tags = new List<Tag>();
        public virtual IList<Tag> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private IList<Person> _authors = new List<Person>();
        public virtual IList<Person> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private IList<Person> _translators = new List<Person>();
        public virtual IList<Person> Translators 
        {
            get { return _translators; }
            set { _translators = value; }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
