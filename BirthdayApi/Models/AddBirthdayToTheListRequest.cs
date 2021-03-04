using System;

namespace BirthdayTracker.Web.Models
{
    public class AddBirthdayToTheListRequest
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
