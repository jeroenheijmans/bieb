using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;

namespace Bieb.Web.Models.Stories
{
    public class ViewStoryModelMapper : IViewEntityModelMapper<Story, ViewStoryModel>
    {
        private readonly IIsbnLanguageDisplayer isbnLanguageDisplayer;

        public ViewStoryModelMapper(IIsbnLanguageDisplayer isbnLanguageDisplayer)
        {
            this.isbnLanguageDisplayer = isbnLanguageDisplayer;
        }

        public ViewStoryModel ModelFromEntity(Story entity)
        {
            return new ViewStoryModel(entity)
                       {
                           Language = isbnLanguageDisplayer.GetLocalizedIsbnLanguageResource(entity.IsbnLanguage)
                       };
        }
    }
}