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

            FullName = entity.FullName;
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

        public string FullName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(BiebResources.PeopleStrings))]
        public Gender Gender { get; set; }

        [Display(Name = "Title", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Title { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string FirstName { get; set; }

        [Display(Name = "Prefix", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Prefix { get; set; }

        [Required]
        [Display(Name = "Surname", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Surname { get; set; }

        [Display(Name = "Nationality", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Nationality { get; set; }

        [Display(Name = "DiedAt", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string PlaceOfDeath { get; set; }

        [Display(Name = "BornAt", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Year", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathYear { get; set; }

        [Display(Name = "Month", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathMonth { get; set; }

        [Display(Name = "Day", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathDay { get; set; }

        [Display(Name = "Year", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthYear { get; set; }

        [Display(Name = "Month", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthMonth { get; set; }

        [Display(Name = "Day", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthDay { get; set; }
    }
}