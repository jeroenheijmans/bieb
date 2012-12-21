using System;
using System.Globalization;

namespace Bieb.Domain.CustomDataTypes
{
    // See also: http://stackoverflow.com/questions/6431908/how-to-design-date-of-birth-in-db-and-orm-for-mix-of-known-and-unkown-dates
    public struct UncertainDate
    {
        public int? Year;
        public int? Month;
        public int? Day;

        public UncertainDate(int? year = null, int? month = null, int? day = null)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
        }

        public UncertainDate(DateTime? from, DateTime? until)
            : this()
        {
            if (from.HasValue && until.HasValue)
            {
                if (from.Value.Year == until.Value.Year)
                {
                    this.Year = from.Value.Year;
                    if (from.Value.Month == until.Value.Month)
                    {
                        this.Month = from.Value.Month;
                        if (from.Value.Day == until.Value.Day) this.Day = from.Value.Day;
                    }
                }
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

        public bool IsCertain
        {
            get
            {
                return Day.HasValue &&
                       Month.HasValue &&
                       Year.HasValue;
            }
        }
    }
}
