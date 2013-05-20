using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditPersonModel : BaseDomainObjectModel<Person>
    {
        public EditPersonModel() : base() 
        { }

        public EditPersonModel(Person entity) : base(entity)
        {
            Title = entity.Title;
            FirstName = entity.FirstName;
            Prefix = entity.Prefix;
            Surname = entity.Surname;
        }

        protected override Person MergeWithEntitySpecifics(Person existingEntity)
        {
            existingEntity.Title = Title;
            existingEntity.FirstName = FirstName;
            existingEntity.Prefix = Prefix;
            existingEntity.Surname = Surname;

            return existingEntity;
        }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Prefix { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}