using System.ComponentModel.DataAnnotations;

namespace BirthdayTracker.Database.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BirthdayInfo BirthdayInfo { get; set; }
    }
}
