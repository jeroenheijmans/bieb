using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class SeriesModel : BaseDomainObjectCrudModel<Series>
    {
        public SeriesModel() : base()
        { }

        public SeriesModel(Series entity) : base(entity)
        {
            Title = entity.Title;
            Subtitle = entity.Subtitle;
        }

        [Display(Name = "Title", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Subtitle { get; set; }

        protected override Series MergeWithEntitySpecifics(Series existingEntity)
        {
            existingEntity.Title = Title;
            existingEntity.Subtitle = Subtitle;

            return existingEntity;
        }
    }
}
