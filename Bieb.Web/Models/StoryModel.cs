using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class StoryModel : BaseDomainObjectModel<Story>
    {
        protected override Story MergeWithEntitySpecifics(Story existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}