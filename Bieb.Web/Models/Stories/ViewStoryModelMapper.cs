using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;

namespace Bieb.Web.Models.Stories
{
    public class ViewStoryModelMapper : IViewEntityModelMapper<Story, ViewStoryModel>
    {
        private readonly IIso639LanguageDisplayer iso639LanguageDisplayer;

        public ViewStoryModelMapper(IIso639LanguageDisplayer iso639LanguageDisplayer)
        {
            this.iso639LanguageDisplayer = iso639LanguageDisplayer;
        }

        public ViewStoryModel ModelFromEntity(Story entity)
        {
            return new ViewStoryModel(entity)
                       {
                           Language = iso639LanguageDisplayer.GetLocalizedIso639LanguageResource(entity.Iso639LanguageId)
                       };
        }
    }
}