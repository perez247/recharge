using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Payment
{
    public class CardRequestResource
    {
        [Required, CreditCard(ErrorMessage="Inalid credit card number")]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9-_ ]+$", ErrorMessage="Only alphabets, numbers _ and - for card holder's name ")]
        public string CardHolderName { get; set; }
        [Required, RegularExpression("[0-9]{3}$", ErrorMessage="Invalid NVV Number")]
        public string CVVNumber { get; set; }
        [Required]
        [RegularExpression("([1-9]|10|11|12)$", ErrorMessage="Invalid Month")]
        public string ExpiryMonth { get; set; }
        [Required]
        [RegularExpression("20[0-9]{2}$", ErrorMessage="Invalid Year")]
        public string ExpiryYear { get; set; }
        
        [Required]
        public Boolean SaveCard { get; set; }

        public string Validate() {
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (isValid == false) {
                // StringBuilder sbrErrors = new StringBuilder();
                // var errorList = new List<string>();
                string errorList = "";
                foreach (var validationResult in results) {
                    errorList += validationResult.ErrorMessage + "%n%";
                    // errorList.Add(validationResult.ErrorMessage);
                    // sbrErrors.AppendJoin(",", validationResult.ErrorMessage);
                }
                // throw new Exception(sbrErrors.ToString());
                return errorList;
            }
            else
                return null;
        }
    }
}