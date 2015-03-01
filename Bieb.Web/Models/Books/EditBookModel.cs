﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Books
{
    public class EditBookModel : EditEntityModel<Book>
    {
        public EditBookModel()
        {
            EditorIds = new int[0];
            AuthorIds = new int[0];
            TranslatorIds = new int[0];
            Stories = new List<EditStoryModel>();
            NewStories = Enumerable.Range(0, 10).Select(x => new EditStoryModel()).ToList();
        }


        [Display(Name = "Isbn", Prompt = "IsbnPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public string Isbn { get; set; }

        [Display(Name = "Title", Prompt = "TitlePlaceholder", ResourceType = typeof (BiebResources.BookStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", Prompt = "SubtitlePlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public string Subtitle { get; set; }

        [Display(Name = "Year", Prompt = "YearPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? Year { get; set; }


        public SelectList AvailableLibraryStatuses { get; set; }

        [Display(Name = "LibraryStatus", ResourceType = typeof(BiebResources.BookStrings))]
        public LibraryStatus LibraryStatus { get; set; }


        public SelectList AvailableIsbnLanguages { get; set; }
        
        [Display(Name = "IsbnLanguage", Prompt = "IsbnLanguagePlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? IsbnLanguage { get; set; }


        public SelectList AvailablePublishers { get; set; }

        [Display(Name = "PublishedBy", ResourceType = typeof(BiebResources.BookStrings))]
        public int? PublisherId { get; set; }


        public SelectList AvailablePeople { get; set; }

        [Display(Name = "Editors", ResourceType = typeof(BiebResources.BookStrings))]
        public int[] EditorIds { get; set; } 

        [Display(Name = "Authors", ResourceType = typeof(BiebResources.BookStrings))]
        public int[] AuthorIds { get; set; }

        [Display(Name = "Translators", ResourceType = typeof(BiebResources.BookStrings))]
        public int[] TranslatorIds { get; set; }


        [Display(Name = "Stories", ResourceType = typeof(BiebResources.BookStrings))]
        public List<EditStoryModel> Stories { get; set; }

        // I'm not happy with this solution, but it allows me to postpone setting up client side code for editing items, so it'll have to do for now.
        [Display(Name = "NewStories", ResourceType = typeof(BiebResources.BookStrings))]
        public List<EditStoryModel> NewStories { get; set; }


        [Display(Name = "ReviewText", Prompt = "ReviewTextPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }
    }
}