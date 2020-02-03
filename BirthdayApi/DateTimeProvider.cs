using System;

namespace BirthdayApi
{
    public interface IDateTimeProvider
    {
        public DateTime GetCurrentDateTime();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}
