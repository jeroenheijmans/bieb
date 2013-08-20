using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditBookModel : EditEntityModel<Book>
    {
        public EditBookModel() : base()
        { }

        public EditBookModel(Book entity) : base(entity)
        {
            Isbn = entity.Isbn;
            IsbnLanguage = entity.IsbnLanguage;
            Title = entity.Title;
            Subtitle = entity.Subtitle;
            Year = entity.Year;
            LibraryStatus = entity.LibraryStatus;
            Publisher = entity.Publisher;
        }

        [Display(Name = "Isbn", Prompt = "IsbnPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public string Isbn { get; set; }

        [Display(Name = "Title", Prompt = "TitlePlaceholder", ResourceType = typeof (BiebResources.BookStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", Prompt = "SubtitlePlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public string Subtitle { get; set; }

        [Display(Name = "Year", Prompt = "YearPlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? Year { get; set; }

        [Display(Name = "LibraryStatus", ResourceType = typeof(BiebResources.BookStrings))]
        public LibraryStatus LibraryStatus { get; set; }

        [Display(Name = "IsbnLanguage", Prompt = "IsbnLanguagePlaceholder", ResourceType = typeof(BiebResources.BookStrings))]
        public int? IsbnLanguage { get; set; }

        [Display(Name = "PublishedBy", ResourceType = typeof(BiebResources.BookStrings))]
        public Publisher Publisher { get; set; }

        protected override Book MergeWithEntitySpecifics(Book existingEntity)
        {
            existingEntity.Isbn = Isbn;
            existingEntity.IsbnLanguage = IsbnLanguage;
            existingEntity.Title = Title;
            existingEntity.Subtitle = Subtitle;
            existingEntity.Year = Year;
            existingEntity.LibraryStatus = LibraryStatus;
            // existingEntity.Publisher = Publisher; // TODO

            return existingEntity;
        }
    }
}