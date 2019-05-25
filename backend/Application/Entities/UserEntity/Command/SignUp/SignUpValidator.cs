using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.CountryCode).NotEmpty();
            RuleFor(x => x).Must(BeAValidMobileNumber).WithName("Phone").WithMessage("Phone number is invalid");
        }

        private bool BeAValidMobileNumber(SignUpCommand signUpCommand) {
            var CountryCode = signUpCommand.CountryCode;
            var Phone = signUpCommand.Phone;

            if (!_codeNumbers.ContainsKey(CountryCode))
                return false;

            var validator = _codeNumbers.GetValueOrDefault(CountryCode);

            if(validator == null) 
                return false;
            
            return validator.Match(Phone).Success;
        }
    }
}