using FluentValidation;
using NeoSoft.Masterminds.Models.Incoming;

namespace NeoSoft.Masterminds.Validators
{
    public class PasswordValidation : AbstractValidator<ChangePasswordModel>
    {
        public PasswordValidation()
        {
           RuleFor(pass => pass.Email)
          .NotNull()
          .NotEmpty()
          .WithMessage("Email is empty")
          .EmailAddress()
          .WithMessage("Incorrect email address");

            RuleFor(pass => pass.CurrentPassword)
                .NotNull()
                .NotEmpty()
                .Must(ValidationHelper.ProperPassword)
                .WithMessage("Write current password");

            RuleFor(pass => pass.NewPassword)
               .NotNull()
               .NotEmpty()
               .Must(ValidationHelper.ProperPassword)
               .WithMessage("Password must have at least one a-z, A-Z, 0-9, '#@)₴?$0' and it is length >=6 ");
        }
    }
}
