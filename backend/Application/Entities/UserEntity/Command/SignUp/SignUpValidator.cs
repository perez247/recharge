using System.Collections.Generic;
using System.Text.RegularExpressions;
using Application.Infrastructure.Validations;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Entities.UserEntity.Command.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"^\d{7}$").WithMessage("Enter a valid numeric pin of lenght 7");
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Must(AppPhone.BeAValidMobileNumber).WithMessage("User's Phone number is invalid");
            RuleFor(x => x.ReferersPhone).NotEmpty().Must(AppPhone.BeAValidMobileNumber).WithMessage("Referer's Phone number is invalid");
            RuleFor(x => x).Must(AppPhone.HaveEqualCountryCode).WithMessage("User must use the same country code number");
        }
    }
}