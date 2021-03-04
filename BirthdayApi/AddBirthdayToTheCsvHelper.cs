using BirthdayTracker.Web.CsvParser;
using BirthdayTracker.Web.Models;

namespace BirthdayTracker.Web
{
    public interface IAddBirthdayHelper
    {
        public void AddNewBirthdayPerson(AddBirthdayToTheListRequest addBirthdayToTheListRequest);
    }

    public class AddBirthdayToCsvHelper : IAddBirthdayHelper
    {
        ICsvWriterWrapper csvWriterWrapper;

        public AddBirthdayToCsvHelper(ICsvWriterWrapper csvWriterWrapper)
        {
            this.csvWriterWrapper = csvWriterWrapper;
        }

        public void AddNewBirthdayPerson(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
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
                DayOfBirth = addBirthdayToTheListRequest.DateOfBirth
            };
        }
    }
}
