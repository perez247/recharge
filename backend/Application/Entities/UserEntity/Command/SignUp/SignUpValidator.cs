using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Entities.UserEntity.Command.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        readonly Dictionary<string, Regex> _codeNumbers = new Dictionary<string, Regex>() {
            {"234", new Regex(@"^([0]{1})([7-9]{1})([0|1]{1})([\d]{1})([\d]{7,8})$")}
        };
        public SignUpValidator()
        {
            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"^\d{7}$").WithMessage("Enter a valid numeric pin of lenght 7");
            // RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Must(BeAValidMobileNumber).WithMessage("User's Phone number is invalid");;
            // RuleFor(x => x).Must(BeAValidMobileNumber).WithName("Phone").WithMessage("Phone number is invalid");
        }

        private bool BeAValidMobileNumber(string phoneNumberString) {
            var phoneNumber = (PhoneNumber)phoneNumberString;

            if (!_codeNumbers.ContainsKey(phoneNumber.CountryCode))
                return false;

            var validator = _codeNumbers.GetValueOrDefault(phoneNumber.CountryCode);

            if(validator == null) 
                return false;
            
            return validator.Match(phoneNumber.Phone).Success;
        }
    }
}