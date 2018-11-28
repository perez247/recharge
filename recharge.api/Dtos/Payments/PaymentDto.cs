using System;
using System.ComponentModel.DataAnnotations;
using recharge.api.Helpers.AnnotationValidation;
using recharge.api.models;

namespace recharge.api.Dtos.Payments
{
    public class PaymentDto
    {
        [RequiredAmount(ErrorMessage="Point should be either 0 or 100 - 50000")]
        public Decimal? Point { get; set; }

        [RequiredAmount(ErrorMessage="Card should be either 0 or 100 - 50000")]
        public Decimal? CardAmount { get; set; }

        public string CardId { get; set; }
        public Card NewCard { get; set; }
        public Boolean SaveCard { get; set; }
        
        [Required(ErrorMessage="Pin is required"), 
        RegularExpression("^[0-9]+$", ErrorMessage="Invalid Pin"),
        StringLength(8, MinimumLength=8, ErrorMessage="Invalid Pin lenght")]
        public string Pin { get; set; }
    }
}