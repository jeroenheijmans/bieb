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

        public DateTime? FromDate
        {
            get
            {
                if (Year.HasValue)
                    return new DateTime(Year.Value, Month ?? 1, Day ?? 1);

                return null;
            }
        }

        public DateTime? UntilDate
        {
            get
            {
                if (Year.HasValue)
                {
                    var month = Month ?? 12;
                    var day = Day ?? DateTime.DaysInMonth(Year.Value, month);
                    return new DateTime(Year.Value, month, day);
                }

                return null;
            }
        }

        public override string ToString()
        {
            if (Year.HasValue && Month.HasValue && Day.HasValue)
                return new DateTime(Year.Value, Month.Value, Day.Value).ToShortDateString();

            if (Year.HasValue && Month.HasValue)
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month.Value) + " " + Year.ToString();

            if (Year.HasValue)
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
