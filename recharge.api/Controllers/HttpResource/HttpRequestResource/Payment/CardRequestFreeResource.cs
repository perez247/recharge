using System;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Payment
{
    public class CardRequestFreeResource
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVVNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public Boolean SaveCard { get; set; }
    }
}