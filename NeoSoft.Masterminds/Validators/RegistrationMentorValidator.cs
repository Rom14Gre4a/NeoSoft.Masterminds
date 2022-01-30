using FluentValidation;
using NeoSoft.Masterminds.Models.Registration;

namespace NeoSoft.Masterminds.Validators
{
    public class RegistrationMentorValidator : AbstractValidator<IncomMentorRegistration>
    {
        public RegistrationMentorValidator()
        {
            RuleFor(m =>m.HourlyRate)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.Professions)
                .NotNull()
                .NotEmpty();

            RuleFor(m => m.LastName)
              .NotNull()
              .NotEmpty()
              .Must(ValidationHelper.ProperName).WithMessage("LastName must contain only letters");

            RuleFor(m => m.Email)
              .NotNull()
              .NotEmpty()
              .EmailAddress();

            RuleFor(m => m.Password)
              .NotNull()
              .NotEmpty()
              .Must(ValidationHelper.ProperPassword)
              .WithMessage("Password must have at least one a-z, A-Z, 0-9, '#@)₴?$0' and it is length >=6 ");



        }
    }
}
