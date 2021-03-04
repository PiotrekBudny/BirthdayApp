using System;
using System.ComponentModel.DataAnnotations;

namespace BirthdayTracker.Database.Models
{
    public class BirthdayInfo
    {
        [Key]
        public int BirthdayId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}