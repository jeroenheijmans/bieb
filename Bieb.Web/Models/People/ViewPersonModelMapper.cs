using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
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
                           IsGenderKnown = entity.Gender != Gender.Unkown,
                           Gender = GenderCharacter(entity.Gender),
                           IsNationalityKnown = !string.IsNullOrEmpty(entity.Nationality),
                           Nationality = entity.Nationality,
                           Roles = string.Join(", ", entity.Roles), // TODO: Localize this
                           IsPlaceOfBirthKnown = !string.IsNullOrEmpty(entity.PlaceOfBirth),
                           PlaceOfBirth = entity.PlaceOfBirth,
                           DateOfBirth = entity.DateOfBirth.ToString(),
                           IsPlaceOfDeathKnown = !string.IsNullOrEmpty(entity.PlaceOfDeath),
                           PlaceOfDeath = entity.PlaceOfDeath,
                           DateOfDeath = entity.DateOfDeath.ToString(),
                           HasTags = entity.Tags.Any(),
                           Tags = string.Join(", ", entity.Tags),
                           Reviews = entity.Reviews.Select(r => new ViewPersonReviewModel(r)),
                           AuthoredBooks = entity.AuthoredBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           EditedBooks = entity.EditedBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           TranslatedBooks = entity.TranslatedBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           TranslatedStories = entity.TranslatedStories.Select(s => storyMapper.ModelFromEntity(s)),
                           AuthoredStories = entity.AuthoredStories.Select(s => storyMapper.ModelFromEntity(s))
                       };
        }

        private static char GenderCharacter(Gender gender)
        {
            switch (gender)
            {
                case Gender.None:
                    return '-';
                case Gender.Male:
                    return '♂';
                case Gender.Female:
                    return '♀';
                case Gender.Unkown:
                default:
                    return '?';
            }
        }
    }
}