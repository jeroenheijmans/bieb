using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class PublisherModel : BaseDomainObjectCrudModel<Publisher>
    {
        public PublisherModel() : base()
        { }

        public PublisherModel(Publisher entity) : base(entity)
        {
            Name = entity.Name;
        }

        [Display(Name = "Name", ResourceType = typeof(BiebResources.PublisherStrings))]
        public string Name { get; set; }

        protected override Publisher MergeWithEntitySpecifics(Publisher existingEntity)
        {
            existingEntity.Name = Name;

            return existingEntity;
        }
    }
}