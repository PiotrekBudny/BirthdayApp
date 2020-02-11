
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
        ICsvWriterWrapper csvWriterWrapper;

        public AddBirthdayToTheListHelper(ICsvWriterWrapper csvWriterWrapper)
        {
            this.csvWriterWrapper = csvWriterWrapper;
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
