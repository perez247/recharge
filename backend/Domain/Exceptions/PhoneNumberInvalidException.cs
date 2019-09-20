using System;

namespace Domain.Exceptions
{
    public class PhoneNumberInvalidException : Exception
    {
        public PhoneNumberInvalidException(string PhoneNumber, Exception ex)
            :base($"Phone Number \" {PhoneNumber} \" is invalid", ex)
        {}
    }
}