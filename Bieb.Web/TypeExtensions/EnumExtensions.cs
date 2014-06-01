using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bieb.Web.TypeExtensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new SelectListItem() { Value = e.ToString(), Text = Enum.GetName(typeof(TEnum), e) };

            return values;
        }
    }
}
