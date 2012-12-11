using System.Collections.Generic;

namespace Bieb.Domain.Entities
{
    public class Story : Publishable
    {
        public virtual Book Book { get; set; }
        public virtual int PositionInBook { get; set; }

        public override int? IsbnLanguage
        {
            get
            {
                if (_isbnLanguage == null && Book != null)
                    return Book.IsbnLanguage;

                return _isbnLanguage;
            }
            set
            {
                _isbnLanguage = value;
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

        public virtual Story ReferenceStory
        {
            get;
            set;
        }


        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}
