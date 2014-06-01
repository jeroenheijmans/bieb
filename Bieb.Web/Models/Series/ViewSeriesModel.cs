using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Models.Books;

namespace Bieb.Web.Models.Series
{
    public class ViewSeriesModel : ViewEntityModel<Domain.Entities.Series>
    {
        public ViewSeriesModel(Domain.Entities.Series series)
            : base(series)
        { }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IEnumerable<ViewBookModel> Books { get; set; }
    }
}