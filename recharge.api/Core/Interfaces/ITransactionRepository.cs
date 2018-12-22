using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Core.Interfaces
{
    public interface ITransactionRepository
    {
         void RecordTransaction(UserTransaction userTransaction, RechargeRequestResource rechargeRequestResource);
        //  void RecordPublicTransaction(PublicPaymentTransaction transaction, User user);
        IEnumerable<UserTransaction> GetUserTransaction(string userId);
        IEnumerable<UserTransaction> GetRefererTransaction(string userId);
        Task<Decimal> GetUsersPoint(string userId);
        // IEnumerable<PublicPaymentTransaction> GetRefererTransaction(string userId);
    }
}