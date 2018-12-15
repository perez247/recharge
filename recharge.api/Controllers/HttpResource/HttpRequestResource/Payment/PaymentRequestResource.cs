using System;
using System.ComponentModel.DataAnnotations;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Helpers.AnnotationValidation;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Payment
{
    public class PaymentRequestResource
    {
        [RequiredAmount(ErrorMessage="Point should be either 0 or 100 - 50000")]
        public Decimal? Point { get; set; }

        [RequiredAmount(ErrorMessage="Card should be either 0 or 100 - 50000")]
        public Decimal? CardAmount { get; set; }

        public string CardId { get; set; }
        public CardRequestFreeResource NewCard { get; set; }
        
        [Required(ErrorMessage="Pin is required"), 
        RegularExpression("^[0-9]+$", ErrorMessage="Invalid Pin"),
        StringLength(8, MinimumLength=8, ErrorMessage="Invalid Pin lenght")]
        public string Pin { get; set; }
    }
}