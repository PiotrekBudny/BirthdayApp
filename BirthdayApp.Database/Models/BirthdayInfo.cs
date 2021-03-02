using System.ComponentModel.DataAnnotations;

namespace BirthdayApp.Database.Models
{
    public class BirthdayInfo
    {
        [Key]
        public int BirthdayId { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public User User { get; set; }
    }
}