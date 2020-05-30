namespace BirthdayApp.Database.Models
{
    public class BirthdayInfo
    {
        public int BirthdayId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public User User { get; set; }
    }
}