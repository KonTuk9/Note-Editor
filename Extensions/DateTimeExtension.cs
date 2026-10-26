namespace Noteeditor.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToStringRelative(this DateTime dateTime)
        {
            DateTime today = DateTime.Today;

            if (dateTime.Date == today)
            {
                return DateTimeExpressions.Today;
            }
            if (dateTime.Date.AddDays(1) == today)
            {
                return DateTimeExpressions.Yesterday;
            }
            if (dateTime.Date.AddDays(7) >= today && dateTime.Month == today.Month)
            {
                return DateTimeExpressions.ThisWeek;
            }
            if (dateTime.Month == today.Month && dateTime.Year == today.Year)
            {
                return DateTimeExpressions.ThisMonth;
            }
            if (dateTime.Year == today.Year)
            {
                string month = new DateTime(dateTime.Year, dateTime.Month, 1).ToString("MMMM");

                return char.ToUpper(month[0]) + month.Substring(1);
            }

            return dateTime.Year.ToString();
        }
    }
}
