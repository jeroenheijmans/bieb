using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class SeriesModel : BaseDomainObjectModel<Series>
    {
        protected override Series MergeWithEntitySpecifics(Series existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}
