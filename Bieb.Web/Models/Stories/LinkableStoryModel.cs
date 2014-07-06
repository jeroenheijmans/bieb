using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Stories
{
    public class LinkableStoryModel : LinkableEntityModel<Story>
    {
        public LinkableStoryModel(Story story)
        {
            Id = story.Id;
            Text = story.Title;
        }
    }

    public static class LinkableStoryModelExtensions
    {
        public static LinkableStoryModel AsLinkableStoryModel(this Story story)
        {
            return story == null
                       ? null
                       : new LinkableStoryModel(story);
        }
    }
}