using FluentValidation;
using NeoSoft.Masterminds.Models.Incoming;

namespace NeoSoft.Masterminds.Validators
{
    public class LoginValidator : AbstractValidator<IncomLogin>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
           .NotNull()
           .NotEmpty()
           .WithMessage("Email is empty")
           .EmailAddress()
           .WithMessage("Incorrect email address");

            RuleFor(login => login.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Write password");
        }
    }
}
