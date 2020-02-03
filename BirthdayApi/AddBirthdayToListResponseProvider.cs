using BirthdayApi.Models;

namespace BirthdayApi
{
    public interface IAddBirthdayToListResponseProvider
    {
        public AddBirthdayToTheListResponse GetBadRequstResponse();

        public AddBirthdayToTheListResponse GetCreatedResponse();
    }

    public class AddBirthdayToListResponseProvider : IAddBirthdayToListResponseProvider
    {
        public AddBirthdayToTheListResponse GetBadRequstResponse()
        {
            return new AddBirthdayToTheListResponse { BirthdayAdded = false, Message = "Bad request" };
        }

        public AddBirthdayToTheListResponse GetCreatedResponse()
        {
            return new AddBirthdayToTheListResponse { BirthdayAdded = true };
        }

    }
}
