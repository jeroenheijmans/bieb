using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditPublisherModel : EditEntityModel<Publisher>
    {
        [Required]
        [Display(Name = "Name", Prompt = "NamePlaceholder", ResourceType = typeof(BiebResources.PublisherStrings))]
        public string Name { get; set; }
    }
}