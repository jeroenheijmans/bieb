using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class PublisherModel : BaseDomainObjectModel<Publisher>
    {
        protected override Publisher MergeWithEntitySpecifics(Publisher existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}