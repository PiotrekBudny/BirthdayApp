using System;
using BirthdayTracker.Web.Providers;
using BirthdayTracker.Web.Utils;

namespace BirthdayTracker.Web.Validators
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
            DateTime birthday;
            
            try
            {
                birthday = StringFormatter.ParseToDateTimeWithoutTime(dateTime);
            }
            catch(Exception exception)
            {
                return false;
            }

            return currentDate.Day == birthday.Day && currentDate.Month == birthday.Month;
        }
    }
}
