using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Publishers;

namespace Bieb.Web.Models.Stories
{
    public class ViewStoryModel : ViewEntityModel<Story>
    {
        public ViewStoryModel(Story story) : base(story)
        {
            Title = story.Title;
            Subtitle = story.Subtitle;
            Year = story.Year;
            HasLanguage = story.IsbnLanguage.HasValue;
            HasTags = story.Tags.Any();
            Tags = string.Join(", ", story.Tags);
            Book = story.Book.AsLinkableBookModel();
            Publisher = story.Publisher.AsLinkablePublisherModel();
            Authors = story.Authors.Select(a => a.AsLinkablePersonModel());
            Translators = story.Translators.Select(t => t.AsLinkablePersonModel());
            ReferenceStory = story.ReferenceStory.AsViewStoryModel();
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int? Year { get; set; }
        public bool HasLanguage { get; set; }
        public string Language { get; set; }

        public bool HasTags { get; set; }
        public string Tags { get; set; }

        public LinkableBookModel Book { get; set; }
        public LinkablePublisherModel Publisher { get; set; }
        public IEnumerable<LinkablePersonModel> Authors { get; set; }
        public IEnumerable<LinkablePersonModel> Translators { get; set; }

        public ViewStoryModel ReferenceStory { get; set; }
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