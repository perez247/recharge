using System;
using System.ComponentModel.DataAnnotations;

namespace recharge.api.Dtos
{
    public class RegisterUserDto
    {
        public RegisterUserDto()
        {
            Expires = DateTime.Now.AddDays(61);
        }

        [Required]
        [RegularExpression("^[a-zA-Z0-9-_ ]+$", ErrorMessage="Only alphabets, numbers _ and - for username ")]
        public string UserName { get; set; }

        [Required, StringLength(12,MinimumLength=10, ErrorMessage= "Phone length between 10 and 12"),
        RegularExpression("^[\\+\\-]?\\d*\\.?\\d+(?:[Ee][\\+\\-]?\\d+)?$", ErrorMessage = "Phone must be numbers")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(8,MinimumLength=8, ErrorMessage= "numbers within 8 and 8"), 
        RegularExpression("^[\\+\\-]?\\d*\\.?\\d+(?:[Ee][\\+\\-]?\\d+)?$", ErrorMessage = "Pin must be numbers")]
        public string Pin { get; set; }
        public DateTime Expires { get; set; }
    }
}