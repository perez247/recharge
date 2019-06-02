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
            // for pin
            RuleFor(x => x.Pin).NotEmpty()
                .Matches(@"^\d{7}$").WithMessage("Enter a valid numeric pin of lenght 7");

            // for username
            RuleFor(x => x.Username).NotEmpty();

            // for user's phone number
            RuleFor(x => x.CountryCode)
                .Must(AppPhone.BeAValidCountryCode).WithMessage("Invalid Counrty Code");

            RuleFor(x => x.PhoneNumber)
                .Must((x, phoneNumber) => AppPhone.BeAValidMobileNumber(x.CountryCode, phoneNumber))
                .WithMessage("User's Phone number is invalid");

            // for referer's phone number
            RuleFor(x => x.ReferersCountryCode)
                .Equal(x => x.CountryCode)
                .When(x => !string.IsNullOrEmpty(x.ReferersCountryCode))
                .WithMessage("Referer's Phone number is invalid");
            
            RuleFor(x => x.ReferersPhoneNumber)
                .Must((x, referersPhoneNumber) => AppPhone.BeAValidMobileNumber(x.ReferersCountryCode, referersPhoneNumber))
                .When(x => !string.IsNullOrEmpty(x.ReferersPhoneNumber))
                .WithMessage("User must use the same country code number");
        }
    }
}