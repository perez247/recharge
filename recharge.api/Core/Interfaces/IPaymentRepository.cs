using System;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Internal;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Core.Interfaces
{
    public interface IPaymentRepository
    {
        void ValidatePayementDetail(RechargeRequestResource paymentRequest, User user);
        User ProcessDatabasePayment(RechargeRequestResource paymentRequest, User user);
        event EventHandler<CustomTransactionEventArgs> transactionMade;
   }
}