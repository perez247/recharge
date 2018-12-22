using System;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;

namespace recharge.api.Helpers.Functions
{
    public static class TransactionFunctions
    {
        public static AppTransaction GenerateAppTransaction(string details, Decimal Amount, Boolean isCard = true){
           return new AppTransaction() {
                    CardNumber = details,
                    Amount = (decimal)Amount,
                    Other = isCard ? "card" : "Points"
                };
        }

        public static UserTransaction GenerateUserTransaction(Decimal Amount, User user){
            return new UserTransaction() {
                User = user,
                Amount = Amount
            };
        }
    }
}