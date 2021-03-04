using System;

namespace BirthdayTracker.Web.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get { return DateTime.UtcNow; } }
    }
}
