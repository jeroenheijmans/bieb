using System.Collections.Generic;

namespace Bieb.Domain.Entities
{
    public class Story : Publishable
    {
        public Story()
            : base("")
        { }

        public Story(string title)
            : base(title)
        { }

        public virtual Book Book { get; set; }
        public virtual int PositionInBook { get; set; }

        public override int? IsbnLanguage
        {
            get
            {
                if (isbnLanguage == null && Book != null)
                    return Book.IsbnLanguage;

                return isbnLanguage;
            }
            set
            {
                isbnLanguage = value;
            }
        }

        private IList<Tag> tags = new List<Tag>();
        public virtual IList<Tag> Tags
        {
            get { return tags; }
        }

        private IList<Person> authors = new List<Person>();
        public virtual IList<Person> Authors
        {
            get { return authors; }
        }

        private readonly IList<Person> translators = new List<Person>();
        public virtual IList<Person> Translators 
        {
            get { return translators; }
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
