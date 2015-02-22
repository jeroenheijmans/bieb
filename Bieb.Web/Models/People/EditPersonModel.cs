using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class EditPersonModel : EditEntityModel<Person>
    {
        public string FullName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(BiebResources.PeopleStrings))]
        public Gender Gender { get; set; }

        [Display(Name = "Title", Prompt = "TitlePlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Title { get; set; }

        [Display(Name = "FirstName", Prompt = "FirstNamePlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string FirstName { get; set; }

        [Display(Name = "Prefix", Prompt = "PrefixPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Prefix { get; set; }

        [Required]
        [Display(Name = "Surname", Prompt = "SurnamePlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Surname { get; set; }

        [Display(Name = "Nationality", Prompt = "NationalityPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Nationality { get; set; }

        [Display(Name = "DiedAt", Prompt = "DiedAtPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string PlaceOfDeath { get; set; }

        [Display(Name = "BornAt", Prompt = "BornAtPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Year", Prompt = "YearPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathYear { get; set; }

        [Display(Name = "Month", Prompt = "MonthPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathMonth { get; set; }

        [Display(Name = "Day", Prompt = "DayPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? DeathDay { get; set; }

        [Display(Name = "Year", Prompt = "YearPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthYear { get; set; }

        [Display(Name = "Month", Prompt = "MonthPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthMonth { get; set; }

        [Display(Name = "Day", Prompt = "DayPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int? BirthDay { get; set; }

        [Display(Name = "ReviewText", Prompt = "ReviewTextPlaceholder", ResourceType = typeof(BiebResources.PeopleStrings))]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }
    }
}