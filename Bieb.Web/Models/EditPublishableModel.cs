using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditPublishableModel<T> : EditEntityModel<T> where T : BaseEntity
    {
        public EditPublishableModel()
        {
            AuthorIds = new int[0];
            TranslatorIds = new int[0];
        }

        [Display(Name = "Title", ResourceType = typeof(BiebResources.BookStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", ResourceType = typeof(BiebResources.BookStrings))]
        public string Subtitle { get; set; }

        [Display(Name = "Year", Prompt = "YearPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? Year { get; set; }

        public SelectList AvailableIsbnLanguages { get; internal set; }

        [Display(Name = "IsbnLanguage", Prompt = "IsbnLanguagePlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? IsbnLanguage { get; set; }

        public SelectList AvailablePublishers { get; internal set; }

        [Display(Name = "PublishedBy", ResourceType = typeof(BiebResources.BookStrings))]
        public int? PublisherId { get; set; }

        public SelectList AvailablePeople { get; internal set; }

        [Display(Name = "Authors", ResourceType = typeof(BiebResources.BookStrings))]
        public int[] AuthorIds { get; set; }

        [Display(Name = "Translators", ResourceType = typeof(BiebResources.BookStrings))]
        public int[] TranslatorIds { get; set; }
    }
}