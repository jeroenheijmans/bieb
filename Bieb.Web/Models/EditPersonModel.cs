using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.CustomDataTypes;
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
            
            Gender = entity.Gender;
            Nationality = entity.Nationality;

            PlaceOfBirth = entity.PlaceOfBirth;
            PlaceOfDeath = entity.PlaceOfDeath;

            BirthDay = entity.DateOfBirth.Day;
            BirthMonth = entity.DateOfBirth.Month;
            BirthYear = entity.DateOfBirth.Year;

            DeathDay = entity.DateOfDeath.Day;
            DeathMonth = entity.DateOfDeath.Month;
            DeathYear = entity.DateOfDeath.Year;
        }

        protected override Person MergeWithEntitySpecifics(Person existingEntity)
        {
            existingEntity.Title = Title;
            existingEntity.FirstName = FirstName;
            existingEntity.Prefix = Prefix;
            existingEntity.Surname = Surname;

            existingEntity.Gender = Gender;
            existingEntity.Nationality = Nationality;

            existingEntity.PlaceOfBirth = PlaceOfBirth;
            existingEntity.PlaceOfDeath = PlaceOfDeath;

            existingEntity.DateOfBirth = new UncertainDate(BirthYear, BirthMonth, BirthDay);
            existingEntity.DateOfDeath = new UncertainDate(DeathYear, DeathMonth, DeathDay);

            return existingEntity;
        }

        public Gender Gender { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Prefix { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Nationality { get; set; }
        public string PlaceOfDeath { get; set; }
        public string PlaceOfBirth { get; set; }

        public int? DeathYear { get; set; }
        public int? DeathMonth { get; set; }
        public int? DeathDay { get; set; }

        public int? BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthDay { get; set; }
    }
}