using System.Collections.Generic;
using System.Text.RegularExpressions;
using Application.Entities.UserEntity.Command.SignUp;
using Domain.ValueObjects;

namespace Application.Infrastructure.Validations
{
    public static class AppPhone
    {
        private static readonly Dictionary<string, Regex> _codeNumbers = new Dictionary<string, Regex>() {
            // {"234", new Regex(@"^([0]{1})([7-9]{1})([0|1]{1})([\d]{1})([\d]{7,8})$")},
            {"234", new Regex(@"^([7-9]{1})([0|1]{1})([\d]{1})([\d]{7,8})$")}
        };

        public static bool BeAValidCountryCode(string countryCode) {
            countryCode = string.IsNullOrEmpty(countryCode) ? "" : countryCode;
            return _codeNumbers.ContainsKey(countryCode);
        }

        public static bool BeAValidMobileNumber(string countryCode, string phoneNumberString) {
            
            countryCode = string.IsNullOrEmpty(countryCode) ? "" : countryCode;
            if (!_codeNumbers.ContainsKey(countryCode))
                return false;

            var validator = _codeNumbers.GetValueOrDefault(countryCode);

            if(validator == null) 
                return false;
            
            return validator.Match(phoneNumberString).Success;
        }

        public static bool HaveEqualCountryCode(SignUpCommand signUpCommand) {
            var userPhone = (PhoneNumber)signUpCommand.Phone;
            var refererPhone = (PhoneNumber)signUpCommand.ReferersPhone;

            return userPhone.CountryCode.Equals(refererPhone.CountryCode);
        }
    }
}