using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;

namespace Bieb.Web.Models.Stories
{
    public class ViewStoryModel : ViewEntityModel<Story>
    {
        public ViewStoryModel(Story story) : base(story)
        {
            Title = story.Title;
            Book = story.Book.AsLinkableBookModel();
            Authors = story.Authors.Select(a => a.AsLinkablePersonModel());
        }

        public string Title { get; set; }
        public LinkableBookModel Book { get; set; }
        public IEnumerable<LinkablePersonModel> Authors { get; set; }
    }

    public static class ViewStoryModelExtensions
    {
        public static ViewStoryModel AsViewStoryModel(this Story story)
        {
            return story == null
                       ? null
                       : new ViewStoryModel(story);
        }
    }
}