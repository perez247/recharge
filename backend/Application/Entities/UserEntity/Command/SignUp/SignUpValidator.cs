using System.Collections.Generic;
using System.Text.RegularExpressions;
using Application.Infrastructure.Validations;
using Application.Interfaces.IRepositories;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Entities.UserEntity.Command.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator(IAuthRepository auth)
        {
            // for pin
            RuleFor(x => x.Pin).NotEmpty()
                .Matches(@"^\d{5}$").WithMessage("Enter a valid numeric pin of lenght 7");

            // for user's phone number
            RuleFor(x => x.CountryCode)
                .Must(AppPhone.BeAValidCountryCode).WithMessage("Invalid Counrty Code");

            // For user's phone number
            RuleFor(x => x.PhoneNumber)
                .Must((x, phoneNumber) => AppPhone.BeAValidMobileNumber(x.CountryCode, phoneNumber))
                .WithMessage("User's Phone number is invalid");
            
            // For user's phone number again
            RuleFor(x => x.PhoneNumber)
                .MustAsync(async (x, phoneNumber, y) => await auth.UniquePhoneNumber($"{x.CountryCode}-{x.PhoneNumber}"))
                .WithMessage(x => $"{x.CountryCode}-{x.PhoneNumber} has already been taken");

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