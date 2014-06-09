using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class EditPersonReviewModel : EditEntityModel<Review<Person>>
    {
        public int PersonId { get; set; }

        [Display(Name = "Text", ResourceType = typeof(BiebResources.PeopleStrings))]
        public string Text { get; set; }

        [Display(Name = "Rating", ResourceType = typeof(BiebResources.PeopleStrings))]
        public int Rating { get; set; }
    }
}