﻿using System;
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
        [Required]
        [Display(Name = "Title", Prompt = "TitlePlaceholder", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Title { get; set; }

        [Display(Name = "Subtitle", Prompt = "SubtitlePlaceholder", ResourceType = typeof(BiebResources.SeriesStrings))]
        public string Subtitle { get; set; }

        public SelectList AvailableBooks { get; set; }
    }
}
