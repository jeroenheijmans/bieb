using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.People
{
    public class ViewPersonModel : ViewEntityModel<Person>
    {
        public ViewPersonModel(Person entity) : base(entity)
        {
        }

        public string FullName { get; set; }

        public bool IsGenderKnown { get; set; }
        public char Gender { get; set; }

        public bool IsNationalityKnown { get; set; }
        public string Nationality { get; set; }

        public string Roles { get; set; }

        public bool IsPlaceOfBirthKnown { get; set; }
        public string PlaceOfBirth { get; set; }
        public string DateOfBirth { get; set; }

        public bool IsPlaceOfDeathKnown { get; set; }
        public string PlaceOfDeath { get; set; }
        public string DateOfDeath { get; set; }

        public bool HasTags { get; set; }
        public string Tags { get; set; }

        public IEnumerable<ViewPersonReviewModel> Reviews { get; set; }

        public IEnumerable<ViewBookModel> AuthoredBooks { get; set; }
        public IEnumerable<ViewBookModel> EditedBooks { get; set; }
        public IEnumerable<ViewBookModel> TranslatedBooks { get; set; }

        public IEnumerable<ViewStoryModel> TranslatedStories { get; set; }
        public IEnumerable<ViewStoryModel> AuthoredStories { get; set; } 
    }
}