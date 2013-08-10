using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class LibraryBookModel : BaseDomainObjectCrudModel<LibraryBook>
    {
        public LibraryBookModel() : base()
        { }

        public LibraryBookModel(LibraryBook entity) : base(entity)
        {
            Isbn = entity.Isbn;
            IsbnLanguage = entity.IsbnLanguage;
            Title = entity.Title;
            Subtitle = entity.Subtitle;
            Year = entity.Year;
            LibraryStatus = entity.LibraryStatus;
            Publisher = entity.Publisher;
        }

        [Display(Name = "Isbn", ResourceType = typeof(BiebResources.BookStrings))]
        public string Isbn { get; set; }

        [Display(Name = "Title", ResourceType = typeof (BiebResources.BookStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", ResourceType = typeof (BiebResources.BookStrings))]
        public string Subtitle { get; set; }

        [Display(Name = "Year", ResourceType = typeof(BiebResources.BookStrings))]
        public int? Year { get; set; }

        [Display(Name = "LibraryStatus", ResourceType = typeof(BiebResources.BookStrings))]
        public LibraryStatus LibraryStatus { get; set; }

        [Display(Name = "IsbnLanguage", ResourceType = typeof(BiebResources.BookStrings))]
        public int? IsbnLanguage { get; set; }

        [Display(Name = "PublishedBy", ResourceType = typeof(BiebResources.BookStrings))]
        public Publisher Publisher { get; set; }

        protected override LibraryBook MergeWithEntitySpecifics(LibraryBook existingEntity)
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