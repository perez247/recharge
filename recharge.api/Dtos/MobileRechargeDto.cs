using System;
using System.ComponentModel.DataAnnotations;
using recharge.api.Dtos.Payments;
using recharge.api.Helpers.AnnotationValidation;

namespace recharge.api.Dtos
{
    public class MobileRechargeDto
    {
        [Required, StringLength(12, MinimumLength = 10, ErrorMessage = "Phone length between 10 and 12"),
        RegularExpression("^[\\+\\-]?\\d*\\.?\\d+(?:[Ee][\\+\\-]?\\d+)?$", ErrorMessage = "Phone must be numbers")]
        public string PhoneNumber { get; set; }

        // Add custom validation to check for the network selected
        [Required, RequiredNetworks(ErrorMessage="Invalid network")]
        public string Network { get; set; }

        [Required, Range(100, 50000, ErrorMessage="between 100 and 50000")]
        public Decimal amount { get; set; }
        public PaymentDto Payment { get; set; }
    }
}