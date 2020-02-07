using System;

namespace BirthdayApi
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
