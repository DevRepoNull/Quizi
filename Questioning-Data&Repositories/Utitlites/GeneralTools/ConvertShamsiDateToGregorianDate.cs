using System.Globalization;

namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public static class PersianDateConverter
    {
        /// <summary>
        /// تبدیل تاریخ شمسی به DateOnly
        /// </summary>
        public static DateOnly ConvertPersianDateToDateOnly(string persianDate)
        {
            var pc = new PersianCalendar();
            var dateParts = persianDate.Split('/');

            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            var dateTime = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            return DateOnly.FromDateTime(dateTime);
        }

        /// <summary>
        /// تبدیل زمان به TimeOnly
        /// </summary>
        public static TimeOnly ConvertTimeToTimeOnly(string time)
        {
            var timeParts = time.Split(':');

            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            return new TimeOnly(hour, minute);
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        public static string ConvertDateOnlyToPersianDate(DateOnly? date)
        {
            if (date == null) return string.Empty;

            var persianCalendar = new System.Globalization.PersianCalendar();
            var dt = date.Value;

            int year = persianCalendar.GetYear(dt.ToDateTime(TimeOnly.MinValue));
            int month = persianCalendar.GetMonth(dt.ToDateTime(TimeOnly.MinValue));
            int day = persianCalendar.GetDayOfMonth(dt.ToDateTime(TimeOnly.MinValue));

            return $"{year:0000}/{month:00}/{day:00}";
        }

        /// <summary>
        /// تبدیل زمان میلادی به زمان جهانی
        /// </summary>
        public static string ConvertTimeOnlyToTime(TimeOnly? time)
        {
            if (time == null) return string.Empty;

            var t = time.Value;
            return $"{t.Hour:00}:{t.Minute:00}";
        }

        /// <summary>
        /// تبدیل تاریخ شمسی و زمان به DateTime میلادی
        /// </summary>
        public static DateTime ConvertPersianDateTimeToGregorianDateTime(string persianDate, string time)
        {
            var pc = new PersianCalendar();

            var dateParts = persianDate.Split('/');
            var timeParts = time.Split(':');

            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            return pc.ToDateTime(year, month, day, hour, minute, 0, 0);
        }
    }
}