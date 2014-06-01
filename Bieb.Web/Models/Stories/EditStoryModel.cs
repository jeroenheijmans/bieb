using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Stories
{
    public class EditStoryModel : EditEntityModel<Story>
    {
        [Display(Name = "Title", ResourceType = typeof(BiebResources.StoryStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", ResourceType = typeof(BiebResources.StoryStrings))]
        public string Subtitle { get; set; }
    }
}