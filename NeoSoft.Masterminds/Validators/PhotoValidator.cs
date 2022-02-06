using FluentValidation;
using NeoSoft.Masterminds.Models.Incoming;
using System;

namespace NeoSoft.Masterminds.Validators
{
    public class PhotoValidator: AbstractValidator<UploadProfilePhoto>
    {
        public PhotoValidator()
        {
            RuleFor(p => p.Avatar).NotEmpty();

            RuleFor(p => p.Avatar.ContentType).NotEmpty();

            RuleFor(p => p.Avatar.ContentType)
                .Must(contentType =>
                {

                    if (contentType.Equals("image/png", StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    if (contentType.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase))
                        return true;

                    return false;
                })
                .WithMessage("ContentType must be 'image/png' or'image/jpeg' ");

            RuleFor(p => p.Avatar.Length).Must(length => length / Math.Pow(1024, 2) < 15)
                .WithMessage("Photo size must be no more than 15MB");
        }
    }
}
