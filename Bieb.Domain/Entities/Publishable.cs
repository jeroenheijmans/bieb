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
        public virtual Publisher Publisher { get; set; }

        protected int? _isbnLanguage;
        public virtual int? IsbnLanguage { get; set; }

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

        /// <summary>
        /// Returns the full name of the language of the book based on the IsbnLanguage
        /// </summary>
        public virtual string Language
        {
            get
            {
                // TODO: Localize language names.
                // TODO: Create a real conversion. 
                // - Perhaps the Internets already has a conversion list for this? 
                // - Perhaps we need to solve this in the database so it's also available for reporting?
                // - Can we use CultureInfo here?

                if (IsbnLanguage == null)
                    return "Language Unkown";

                switch (IsbnLanguage)
                {
                    case 0:
                    case 1:
                        return "English";
                    case 2:
                        return "French";
                    case 3:
                        return "German";
                    case 4:
                        return "Japanese";
                    case 5:
                        return "Russian";
                    case 90:
                        return "Dutch";
                    default:
                        return "Isbn Country Code not recognized";
                }
            }
        }
    }
}
