using BirthdayApi.Utils;

namespace BirthdayApi.Validators
{
    public interface IBirthdayValidator
    {
        public bool ValidateIfTodayIsPersonBirthday(string dateTime);
    }

    public class BirthdayValidator : IBirthdayValidator
    {
        private DateTimeProvider _dateTimeProvider;

        public BirthdayValidator()
        {
            _dateTimeProvider = new DateTimeProvider();
        }

        public bool ValidateIfTodayIsPersonBirthday(string dateTime)
        {
            var currentDate = _dateTimeProvider.GetCurrentDateTime();

            var birthday = StringFormatter.ParseToDateTimeWithoutTime(dateTime);

            return currentDate.Day == birthday.Day && currentDate.Month == birthday.Month;
        }
    }
}
