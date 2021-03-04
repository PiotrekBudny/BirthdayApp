using BirthdayTracker.Web.Models;
using FluentValidation;

namespace BirthdayTracker.Web.Validators
{
    public class AddBirthdayToListRequestValidator : AbstractValidator<AddBirthdayToTheListRequest>
    {
        public AddBirthdayToListRequestValidator()
        {
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).Must(ValidateFormat);
        }

        public bool ValidateFormat(string value)
        {
            return value.MatchesDateTimeFormat();
        }
    }
}
