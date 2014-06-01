using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Web.Models.Series
{
    public class LinkableSeriesModel : LinkableRootEntityModel<Domain.Entities.Series>
    {
        public LinkableSeriesModel(Domain.Entities.Series series)
        {
            Id = series.Id;
            Text = series.Title;
        }
    }

    public static class LinkableSeriesModelExtensions
    {
        public static LinkableSeriesModel AsLinkableSeriesModel(this Domain.Entities.Series series)
        {
            return series == null
                       ? null
                       : new LinkableSeriesModel(series);
        }
    }
}