using System.Globalization;

namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public static class DateConvertor
    {
        public static string ConvertToShamsi(this DateTime value)
        {
            PersianCalendar persian = new PersianCalendar();
            return persian.GetYear(value).ToString() + "/" +
                   persian.GetMonth(value).ToString("00") + "/" +
                   persian.GetDayOfMonth(value).ToString("00");
        }
    }
}
