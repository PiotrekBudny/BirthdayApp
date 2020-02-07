using BirthdayApi.Models;
using FluentValidation;

namespace BirthdayApi.Validators
{
    public class AddBirthdayToListRequestValidator : AbstractValidator<AddBirthdayToTheListRequest>
    {
        public AddBirthdayToListRequestValidator()
        {
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DayOfBirth).Must(ValidateFormat);
        }

        public bool ValidateFormat(string value)
        {
            return value.MatchesDateTimeFormat();
        }
    }
}
