using System;

namespace BirthdayTracker.Web.Utils
{
    public static class StringFormatter
    {
        public static DateTime ParseToDateTimeWithoutTime(string date) => DateTime.Parse(date).Date;
    }
}
