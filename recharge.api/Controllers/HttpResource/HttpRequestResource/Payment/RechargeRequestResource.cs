using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Payment
{
    public abstract class RechargeRequestResource
    {
        [Required, Range(100, 50000, ErrorMessage="between 100 and 50000")]
        public Decimal Amount { get; set; }
        public PaymentRequestResource Payment { get; set; }

        public abstract string AdditionalInformation();
    }
}