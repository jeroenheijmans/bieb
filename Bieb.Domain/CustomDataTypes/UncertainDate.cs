using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Bieb.Domain.CustomDataTypes
{
    // See also: http://stackoverflow.com/questions/6431908/how-to-design-date-of-birth-in-db-and-orm-for-mix-of-known-and-unkown-dates
    public class UncertainDate
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public UncertainDate(int? Year = null, int? Month = null, int? Day = null)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        public UncertainDate(DateTime? from, DateTime? until)
        {
            if (from.HasValue && until.HasValue)
            {
                if (from.Value.Year == until.Value.Year) this.Year = from.Value.Year;
                if (from.Value.Month == until.Value.Month) this.Month = from.Value.Month;
                if (from.Value.Day == until.Value.Day) this.Day = from.Value.Day;
            }
        }

        public override string ToString()
        {
            if (Year != null && Month != null && Day != null)
                return new DateTime(Year.Value, Month.Value, Day.Value).ToShortDateString();

            if (Year != null && Month != null)
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month.Value) + " " + Year.ToString();

            if (Year != null)
                return Year.ToString();

            return "?";
        }
    }
}
