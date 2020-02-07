using BirthdayApi.CsvParser;
using BirthdayApi.Models;
using BirthdayApi.Validators;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayApi
{   
    public interface IGetBirthdayPeopleDetailsResponseProvider
    {
        GetBirthDayPeopleDetailsResponse GetBirthdaysFilteringByLastName(string lastName);
        GetBirthDayPeopleDetailsResponse GetBirthdaysForToday();
    }

    public class GetBirthdayPeopleDetailsResponseProvider : IGetBirthdayPeopleDetailsResponseProvider
    {
        private CsvReaderWrapper _csvReaderWrapper;
        private BirthdayValidator _birthdayValidator;

        public GetBirthdayPeopleDetailsResponseProvider()
        {
            _csvReaderWrapper = new CsvReaderWrapper();
            _birthdayValidator = new BirthdayValidator();
        }

        public GetBirthDayPeopleDetailsResponse GetBirthdaysFilteringByLastName(string lastName)
        {
            var peoplelist = _csvReaderWrapper.ReadFromBirthDayCsvFile()
                    .FindAll(x => x.LastName == lastName);

            return BuildGetBirthdayPeopleDetailsResponse(peoplelist);
        }

        public GetBirthDayPeopleDetailsResponse GetBirthdaysForToday()
        {
            var peopleList = _csvReaderWrapper.ReadFromBirthDayCsvFile()
                                 .Where((x) => ValidateIfTodayIsSomeonesBirthday(x.DayOfBirth)).ToList();

            return BuildGetBirthdayPeopleDetailsResponse(peopleList);
        }

        private bool ValidateIfTodayIsSomeonesBirthday(string dateToValidate)
        {
            return _birthdayValidator.ValidateIfTodayIsPersonBirthday(dateToValidate);
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
