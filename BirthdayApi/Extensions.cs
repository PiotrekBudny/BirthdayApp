using System;
using System.Globalization;

namespace BirthdayTracker.Web
{
    public static class Extensions
    {
        public static bool MatchesDateTimeFormat(this string value)
        {
            _ = new DateTime();
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
