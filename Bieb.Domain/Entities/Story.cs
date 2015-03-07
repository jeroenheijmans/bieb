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

        public virtual Story ReferenceStory
        {
            get;
            set;
        }

        public override void AddAuthor(Person person)
        {
            authors.Add(person);
            person.AddAuthoredStory(this);
        }

        public override void AddTranslator(Person person)
        {
            translators.Add(person);
            person.AddTranslatedStory(this);
        }

        public override void RemoveAuthor(Person person)
        {
            authors.Remove(person);
            person.RemoveAuthoredStory(this);
        }

        public override void RemoveTranslator(Person person)
        {
            translators.Remove(person);
            person.RemoveTranslatedStory(this);
        }

        public override void ClearAuthors()
        {
            foreach (var person in Authors)
            {
                person.RemoveAuthoredStory(this);
            }

            translators.Clear();
        }

        public override void ClearTranslators()
        {
            foreach (var person in Translators)
            {
                person.RemoveTranslatedStory(this);
            }

            translators.Clear();
        }


        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ? base.ToString() : Title;
        }
    }
}
