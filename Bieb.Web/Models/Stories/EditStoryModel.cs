using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditStoryModel : EditEntityModel<Story>
    {
        [Display(Name = "Title", ResourceType = typeof(BiebResources.StoryStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", ResourceType = typeof(BiebResources.StoryStrings))]
        public string Subtitle { get; set; }
    }
}