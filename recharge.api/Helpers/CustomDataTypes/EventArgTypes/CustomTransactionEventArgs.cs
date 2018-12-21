using System;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;

namespace recharge.api.Helpers.CustomDataTypes.EventArgTypes
{
    public class CustomTransactionEventArgs : EventArgs
    {
        public User User { get; set; }
        public RechargeRequestResource Transaction { get; set; }
    }
}