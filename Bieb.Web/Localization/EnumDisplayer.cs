using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Localization
{
    public static class EnumDisplayer
    {
        public static string GetResource(LibraryStatus status)
        {
            var id = Convert.ChangeType(status, status.GetTypeCode());
            return BiebResources.Enums.ResourceManager.GetString("LibraryStatus" + id);
        }
    }
}