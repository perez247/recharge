using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Infrastructure;

namespace Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Phone { get; set; }
        public string CountryCode { get; set; }

        public override string ToString()
        {
            return $"{Phone}-{CountryCode}";
        }

        private PhoneNumber() {}

        public static implicit operator string(PhoneNumber phoneNumber)
        {
            return phoneNumber.ToString();
        }

        public static explicit operator PhoneNumber(string phoneNumberString)
        {
            return For(phoneNumberString);
        }

        public static PhoneNumber For(string phoneNumberString)
        {
            var phone = new PhoneNumber();

            try
            {
                var index = phoneNumberString.IndexOf("-", StringComparison.Ordinal);
                phone.CountryCode = phoneNumberString.Substring(0, index);
                phone.Phone = phoneNumberString.Substring(index + 1);
            }
            catch (Exception ex)
            {
                throw new PhoneNumberInvalidException(phoneNumberString, ex);
            }

            return phone;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Phone;
            yield return CountryCode;        }
    }
}