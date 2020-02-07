using BirthdayApi.Utils;

namespace BirthdayApi.Validators
{
    public interface IBirthdayValidator
    {
        public bool ValidateIfTodayIsPersonBirthday(string dateTime);
    }

    public class BirthdayValidator : IBirthdayValidator
    {
        public bool ValidateIfTodayIsPersonBirthday(string dateTime)
        {
            var currentDate = new DateTimeProvider().UtcNow;

            var birthday = StringFormatter.ParseToDateTimeWithoutTime(dateTime);

            return currentDate.Day == birthday.Day && currentDate.Month == birthday.Month;
        }
    }
}
