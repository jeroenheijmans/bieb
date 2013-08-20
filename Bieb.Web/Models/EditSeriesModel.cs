using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class EditSeriesModel : EditEntityModel<Series>
    {
        public EditSeriesModel() : base()
        { }

        public EditSeriesModel(Series entity) : base(entity)
        {
            Title = entity.Title;
            Subtitle = entity.Subtitle;
        }

        [Required]
        [Display(Name = "Title", Prompt = "TitlePlaceholder", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", Prompt = "SubtitlePlaceholder", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Subtitle { get; set; }

        protected override Series MergeWithEntitySpecifics(Series existingEntity)
        {
            existingEntity.Title = Title;
            existingEntity.Subtitle = Subtitle;

            return existingEntity;
        }
    }
}
