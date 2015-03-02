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
            return BiebResources.Enums.ResourceManager.GetString("LibraryStatus" + GetEnumId(status));
        }

        public static string GetResource(Gender gender)
        {
            return BiebResources.Enums.ResourceManager.GetString("Gender" + GetEnumId(gender));
        }

        public static string GetResource(Role role)
        {
            return BiebResources.Enums.ResourceManager.GetString("Role" + GetEnumId(role));
        }

        private static object GetEnumId(Enum status)
        {
            return Convert.ChangeType(status, status.GetTypeCode());
        }
    }
}