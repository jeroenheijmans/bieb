using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class ReferenceBookModel : BaseDomainObjectCrudModel<ReferenceBook>
    {
        protected override ReferenceBook MergeWithEntitySpecifics(ReferenceBook existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}