using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Publishers;
using Bieb.Web.Models.Series;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Books
{
    public class ViewBookModelMapper : IViewEntityModelMapper<Book, ViewBookModel>
    {
        private readonly IIso639LanguageDisplayer iso639LanguageDisplayer;

        public ViewBookModelMapper(IIso639LanguageDisplayer iso639LanguageDisplayer)
        {
            this.iso639LanguageDisplayer = iso639LanguageDisplayer;
        }

        public ViewBookModel ModelFromEntity(Book entity)
        {
            var model = new ViewBookModel(entity)
                            {
                                Title = entity.Title,
                                Subtitle = entity.Subtitle,
                                LibraryStatus = EnumDisplayer.GetResource(entity.LibraryStatus),
                                Tags = string.Join(", ", entity.Tags.Select(t => t.Name)),
                                ShowPublishingInfo = (entity.Publisher != null) || (entity.Year.HasValue) || (!string.IsNullOrEmpty(entity.Iso639LanguageId)),
                                Publisher = entity.Publisher.AsLinkablePublisherModel(),
                                Isbn = entity.Isbn,
                                Year = entity.Year,
                                IsLanguageKnown = !string.IsNullOrEmpty(entity.Iso639LanguageId),
                                Language = iso639LanguageDisplayer.GetLocalizedIso639LanguageResource(entity.Iso639LanguageId),
                                Series = entity.Series.AsLinkableSeriesModel(),
                                Editors = entity.Editors.Select(p => p.AsLinkablePersonModel()),
                                Authors = entity.AllAuthors.Select(p => p.AsLinkablePersonModel()),
                                CoverPeople = entity.Editors.Any() ? entity.Editors.Select(p => p.AsLinkablePersonModel()) : entity.AllAuthors.Select(p => p.AsLinkablePersonModel()),
                                Translators = entity.AllTranslators.Select(p => p.AsLinkablePersonModel()),
                                ReviewText =  entity.ReviewText,
                                ShowStoriesList = entity.BookType != BookType.Novel,
                                Stories = entity.Stories.Select(s => new ViewStoryModel(s.Value)),
                                BookType = LocalizeBookType(entity.BookType)
                            };

            if (entity.ReferenceBook != null)
            {
                model.ReferenceBook = ModelFromEntity(entity.ReferenceBook);
            }

            return model;
        }

        private static string LocalizeBookType(BookType bookType)
        {
            switch (bookType)
            {
                case BookType.Anthology:
                    return BiebResources.BookStrings.Anthology;

                case BookType.Collection:
                    return BiebResources.BookStrings.Collection;

                case BookType.Novel:
                default:
                    return BiebResources.BookStrings.Novel;
            }
        }
    }
}