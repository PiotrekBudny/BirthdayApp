using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayApp.Database.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthdayId { get; set; }
        public BirthdayInfo BirthdayInfo { get; set; }
    }
}
