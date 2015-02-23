using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Publishers;
using Bieb.Web.Models.Series;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Books
{
    public class ViewBookModel : ViewEntityModel<Book>
    {
        public ViewBookModel(Book book) : base(book)
        { }

        public string BookType { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public bool ShowPublishingInfo { get; set; }
        public LinkablePublisherModel Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public int? Year { get; set; }
        
        public string Tags { get; set; }

        public LinkableSeriesModel Series { get; set; }

        /// <summary>
        /// Name(s) on the cover (editors for collections, authors for novels and anthologies)
        /// </summary>
        public IEnumerable<LinkablePersonModel> CoverPeople { get; set; } // TODO: Set this in the mapper.

        public IEnumerable<LinkablePersonModel> Editors { get; set; }
        public IEnumerable<LinkablePersonModel> Authors { get; set; }
        public IEnumerable<LinkablePersonModel> Translators { get; set; }

        public ViewBookModel ReferenceBook { get; set; }

        public string ReviewText { get; set; }

        public bool ShowStoriesList { get; set; }
        public IEnumerable<ViewStoryModel> Stories { get; set; } 
    }
}