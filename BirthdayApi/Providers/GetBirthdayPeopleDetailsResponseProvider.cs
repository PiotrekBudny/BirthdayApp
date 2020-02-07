using BirthdayApi.CsvParser;
using BirthdayApi.Models;
using BirthdayApi.Validators;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayApi.Providers
{
    public interface IGetBirthdayPeopleDetailsResponseProvider
    {
        GetBirthDayPeopleDetailsResponse GetBirthdaysFilteringByLastName(string lastName);
        GetBirthDayPeopleDetailsResponse GetBirthdaysForToday();
    }

    public class GetBirthdayPeopleDetailsResponseProvider : IGetBirthdayPeopleDetailsResponseProvider
    {
        ICsvReaderWrapper csvReaderWrapper;
        IBirthdayValidator birthdayValidator;

        public GetBirthdayPeopleDetailsResponseProvider(ICsvReaderWrapper csvReaderWrapper, IBirthdayValidator birthdayValidator)
        {
            this.csvReaderWrapper = csvReaderWrapper;
            this.birthdayValidator = birthdayValidator;
        }

        public GetBirthDayPeopleDetailsResponse GetBirthdaysFilteringByLastName(string lastName)
        {
            var peoplelist = csvReaderWrapper.ReadFromBirthDayCsvFile()
                    .FindAll(x => x.LastName == lastName);

            return BuildGetBirthdayPeopleDetailsResponse(peoplelist);
        }

        public GetBirthDayPeopleDetailsResponse GetBirthdaysForToday()
        {
            var peopleList = csvReaderWrapper.ReadFromBirthDayCsvFile()
                                 .Where((x) => ValidateIfTodayIsSomeonesBirthday(x.DayOfBirth)).ToList();

            return BuildGetBirthdayPeopleDetailsResponse(peopleList);
        }

        private bool ValidateIfTodayIsSomeonesBirthday(string dateToValidate)
        {
            return birthdayValidator.ValidateIfTodayIsPersonBirthday(dateToValidate);
        }

        private GetBirthDayPeopleDetailsResponse BuildGetBirthdayPeopleDetailsResponse(List<BirthdayPerson> peopleList)
        {
            return new GetBirthDayPeopleDetailsResponse()
            {
                BirthdayPeopleList = peopleList
            };
        }
    }
}
