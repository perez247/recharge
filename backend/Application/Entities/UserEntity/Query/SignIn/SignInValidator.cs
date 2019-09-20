using Application.Infrastructure.Validations;
using FluentValidation;

namespace Application.Entities.UserEntity.Query.SignIn
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {

            // for user's phone number
            RuleFor(x => x.CountryCode)
                .Must(AppPhone.BeAValidCountryCode).WithMessage("Invalid Counrty Code");

            // For user's phone number
            RuleFor(x => x.PhoneNumber)
                .Must((x, phoneNumber) => AppPhone.BeAValidMobileNumber(x.CountryCode, phoneNumber))
                .WithMessage("User's Phone number is invalid");

            // for pin
            RuleFor(x => x.Pin).NotEmpty()
                .Matches(@"^\d{5}$").WithMessage("Enter a valid numeric pin of lenght 5");
        }
    }
}