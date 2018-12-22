using System;

namespace recharge.api.Controllers.HttpResource.HttpResponseResource.Transactions
{
    public class UserTransactionResponseResource
    {
        public Decimal UserPoint { get; set; }
        public Decimal Balance { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime DateCreated { get; set; }
    }
}