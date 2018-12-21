using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using recharge.api.Core.Interfaces;
using recharge.api.Helpers.AnnotationValidation;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Payment
{
    public class MobileRechargeRequestResourse : RechargeRequestResource
    {
        [Required, StringLength(12, MinimumLength = 10, ErrorMessage = "Phone length between 10 and 12"),
        RegularExpression("^[\\+\\-]?\\d*\\.?\\d+(?:[Ee][\\+\\-]?\\d+)?$", ErrorMessage = "Phone must be numbers")]
        public string PhoneNumber { get; set; }

        // Add custom validation to check for the network selected
        [Required, RequiredNetworks(ErrorMessage="Invalid network")]
        public string Network { get; set; }
        public override string AdditionalInformation(){
            StringBuilder data = new StringBuilder();
            data.AppendLine("Phone: " + this.PhoneNumber);
            data.AppendLine("Network: "+ this.Network);

            return data.ToString();
        }
    }
}