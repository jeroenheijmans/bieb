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

        // TODO: Evaluate if this property is even necessary.
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

        private readonly ISet<Tag> tags = new HashSet<Tag>();
        public virtual ISet<Tag> Tags
        {
            get { return tags; }
        }

        public virtual Story ReferenceStory
        {
            get;
            set;
        }


        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ? base.ToString() : Title;
        }
    }
}
