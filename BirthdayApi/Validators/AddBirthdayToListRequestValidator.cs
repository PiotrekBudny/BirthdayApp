using BirthdayApi.Models;
using FluentValidation;
using System;
using System.Globalization;

namespace BirthdayApi.Validators
{
    public class AddBirthdayToListRequestValidator : AbstractValidator<AddBirthdayToTheListRequest>
    {
        public AddBirthdayToListRequestValidator()
        {
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DayOfBirth).Must(MatchesDateTimeFormat);
        }

        private bool MatchesDateTimeFormat(string value)
        {
            _ = new DateTime();
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

    }
}
