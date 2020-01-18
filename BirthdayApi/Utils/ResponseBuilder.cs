using BirthdayApi.Models;
using System.Collections.Generic;


namespace BirthdayApi
{
    public static class ResponseBuilder
    {    
        public static GetBirthDayPeopleDetailsResponse BuildGetBirthdayPersonDetailsResponse(List<BirthdayPerson> peopleList)
        {
            return new GetBirthDayPeopleDetailsResponse()
            {
                BirthdayPeopleList = peopleList,
                Count = peopleList.Count
            };
        }
    
    }
}
