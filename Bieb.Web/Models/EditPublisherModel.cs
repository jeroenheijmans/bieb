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
        public EditPublisherModel() : base()
        { }

        public EditPublisherModel(Publisher entity) : base(entity)
        {
            Name = entity.Name;
        }

        [Required]
        [Display(Name = "Name", Prompt = "NamePlaceholder", ResourceType = typeof(BiebResources.PublisherStrings))]
        public string Name { get; set; }

        protected override Publisher MergeWithEntitySpecifics(Publisher existingEntity)
        {
            existingEntity.Name = Name;

            return existingEntity;
        }
    }
}