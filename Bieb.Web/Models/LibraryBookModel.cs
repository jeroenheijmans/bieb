using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models
{
    public class LibraryBookModel : BaseDomainObjectCrudModel<LibraryBook>
    {
        protected override LibraryBook MergeWithEntitySpecifics(LibraryBook existingEntity)
        {
            throw new NotImplementedException();
        }
    }
}