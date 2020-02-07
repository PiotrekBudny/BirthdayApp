using NUnit.Framework;
using BirthdayApi.Validators;
using System;

namespace BirthdayApp.UnitTests
{
    [TestFixture]
    public class BirthdayValidatorTests
    {
        [Test]
        public void Can_Validate_If_Today_Is_Birthday_For_Current_Date_In_Valid_Format()
        {
            //ARRANGE
            var birthdayValidator = new BirthdayValidator();
            var currentDateTimeInValidFormat = DateTime.UtcNow.ToString("dd/MM/yyyy");
            //ACT
            var result = birthdayValidator.ValidateIfTodayIsPersonBirthday(currentDateTimeInValidFormat);

            //ASSERT
            Assert.True(result);
        }

        [Test]
        public void Can_Validate_If_Today_Is_Birthday_For_Not_Current_Date_In_Valid_Format()
        {
            //ARRANGE
            var birthdayValidator = new BirthdayValidator();
            var notCurrentDateTimeInValidFormat = GenerateDateThatIsNotCurrentDate().ToString("dd/MM/yyyy");
            //ACT
            var result = birthdayValidator.ValidateIfTodayIsPersonBirthday(notCurrentDateTimeInValidFormat);

            //ASSERT
            Assert.False(result);
        }
        
        [Test]
        public void Can_Validate_If_Today_Is_Birthday_For_Current_Date_In_Different_Format()
        {
            //ARRANGE
            var birthdayValidator = new BirthdayValidator();
            var currentDateTimeInDifferentFormat = DateTime.UtcNow.ToString("dd-MM-yyyy");
            //ACT
            var result = birthdayValidator.ValidateIfTodayIsPersonBirthday(currentDateTimeInDifferentFormat);

            //ASSERT
            Assert.True(result);
        }

        [Test]
        public void Can_Validate_As_False_If_Today_Is_Birthday_For_String_That_Is_Not_Date()
        {
            //ARRANGE
            var birthdayValidator = new BirthdayValidator();
            var notDateTimeString = "word";
            //ACT

            var result = birthdayValidator.ValidateIfTodayIsPersonBirthday(notDateTimeString);

            //ASSERT
            Assert.False(result);
        }

        private DateTime GenerateDateThatIsNotCurrentDate()
        {
            return DateTime.UtcNow.AddDays(-2).AddMonths(-2);
        }
    }
}