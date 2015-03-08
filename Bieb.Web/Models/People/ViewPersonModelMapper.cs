using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.People
{
    public class ViewPersonModelMapper : IViewEntityModelMapper<Person, ViewPersonModel>
    {
        private readonly IViewEntityModelMapper<Book, ViewBookModel> bookMapper;
        private readonly IViewEntityModelMapper<Story, ViewStoryModel> storyMapper;

        public ViewPersonModelMapper(IViewEntityModelMapper<Book, ViewBookModel> bookMapper, IViewEntityModelMapper<Story, ViewStoryModel> storyMapper)
        {
            this.bookMapper = bookMapper;
            this.storyMapper = storyMapper;
        }

        public ViewPersonModel ModelFromEntity(Person entity)
        {
            return new ViewPersonModel(entity)
                       {
                           FullName = entity.FullName,
                           IsGenderKnown = entity.Gender != Gender.Unknown,
                           Gender = EnumDisplayer.GetResource(entity.Gender),
                           GenderTooltip = GenderTooltip(entity.Gender),
                           IsNationalityKnown = !string.IsNullOrEmpty(entity.Nationality),
                           Nationality = entity.Nationality,
                           Roles = string.Join(", ", entity.Roles.Select(EnumDisplayer.GetResource)),
                           IsPlaceOfBirthKnown = !string.IsNullOrEmpty(entity.PlaceOfBirth),
                           PlaceOfBirth = entity.PlaceOfBirth,
                           IsDateOfBirthKnown = !entity.DateOfBirth.IsCompletelyUnknown,
                           DateOfBirth = entity.DateOfBirth.ToString(),
                           IsPlaceOfDeathKnown = !string.IsNullOrEmpty(entity.PlaceOfDeath),
                           PlaceOfDeath = entity.PlaceOfDeath,
                           IsDateOfDeathKnown = !entity.DateOfDeath.IsCompletelyUnknown,
                           DateOfDeath = entity.DateOfDeath.ToString(),
                           HasTags = entity.Tags.Any(),
                           Tags = string.Join(", ", entity.Tags),
                           ReviewText = entity.ReviewText,
                           AuthoredBooks = entity.AuthoredBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           EditedBooks = entity.EditedBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           TranslatedBooks = entity.TranslatedBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           TranslatedStories = entity.TranslatedStories.Select(s => storyMapper.ModelFromEntity(s)),
                           AuthoredStories = entity.AuthoredStories.Select(s => storyMapper.ModelFromEntity(s))
                       };
        }

        private static string GenderTooltip(Gender gender)
        {
            switch (gender)
            {
                case Gender.None:
                    return BiebResources.PeopleStrings.GenderTooltipNone;
                case Gender.Male:
                    return BiebResources.PeopleStrings.GenderTooltipMale;
                case Gender.Female:
                    return BiebResources.PeopleStrings.GenderTooltipFemale;
                case Gender.Unknown:
                default:
                    return BiebResources.PeopleStrings.GenderTooltipUnknown;
            }
        }
    }
}