using System;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Internal
{
    public class PaymentRequest
    {
        public Decimal amount { get; set; }
        public PaymentRequestResource Payment { get; set; }
    }
}