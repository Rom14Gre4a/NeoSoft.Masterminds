using FluentValidation;
using NeoSoft.Masterminds.Models.Registration;

namespace NeoSoft.Masterminds.Validators
{
    public class RegistrationUserValidator : AbstractValidator<IncomUserRegistration>
    {
        public RegistrationUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Add First name")
                .WithMessage("Incorrect first name(must start with a capital letter)");


                RuleFor(user => user.LastName)
                .NotEmpty()
                .NotNull()
                .Must(ValidationHelper.ProperName)
                .WithMessage("Incorrect first name(must start with a capital letter");

            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Incorrect e-mail");

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password must have at least one a-z, A-Z, 0-9, '#@)₴?$0' and it is length >=6 ");

            RuleFor(user => user.ConfirmPassword)
                .Equal(user => user.Password)
                .WithMessage("Password does not confirm the password");

        }
    }
}
