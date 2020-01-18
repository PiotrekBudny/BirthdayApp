using BirthdayApi.Models;
using BirthdayApi.Utils;
using System;

namespace BirthdayApi
{
    public static class BirthdayValidator
    {
        public static bool ValidateIfTodayIsPersonBirthday(string dateTime)
        {
            var currentDate = DateTime.Now.Date;

            var birthday = StringFormatter.ParseToDateTimeWithoutTime(dateTime);

            return (currentDate.Day == birthday.Day) && (currentDate.Month == birthday.Month);
        }

        public static bool ValidateAddBirthDayToListRequest(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            return string.IsNullOrEmpty(addBirthdayToTheListRequest.FirstName)
                || string.IsNullOrEmpty(addBirthdayToTheListRequest.LastName)
                || addBirthdayToTheListRequest.DayOfBirth.MatchesDateTimeFormat();
        }
    }
}
