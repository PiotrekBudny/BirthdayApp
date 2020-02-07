
using BirthdayApi.CsvParser;
using BirthdayApi.Models;

namespace BirthdayApi
{
    public interface IAddBirthdayToTheListHelper
    {
        public void AddNewBirthdayPersonToCsvfile(AddBirthdayToTheListRequest addBirthdayToTheListRequest);
    }

    public class AddBirthdayToTheListHelper : IAddBirthdayToTheListHelper
    {
        CsvWriterWrapper csvWriterWrapper;

        public AddBirthdayToTheListHelper()
        {
            csvWriterWrapper = new CsvWriterWrapper();
        }

        public void AddNewBirthdayPersonToCsvfile(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            var birthdayPerson = MapRequestToBirthDayPerson(addBirthdayToTheListRequest);

            csvWriterWrapper.WriteToBirthdayCsvFile(birthdayPerson);
        }

        private BirthdayPerson MapRequestToBirthDayPerson(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            return new BirthdayPerson()
            {
                LastName = addBirthdayToTheListRequest.LastName,
                FirstName = addBirthdayToTheListRequest.FirstName,
                DayOfBirth = addBirthdayToTheListRequest.DayOfBirth
            };
        }
    }
}
