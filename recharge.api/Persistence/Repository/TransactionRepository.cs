using System;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDataRepository _context;

        public TransactionRepository(IDataRepository context)
        {
            _context = context;
        }

        public async void RecordTransaction(object source, CustomTransactionEventArgs e)
        {
            var Transaction = new PaymentTransaction() {
                UserId = e.User.Id,
                RefererId = e.User.RefererId,
                Amount = e.Transaction.amount,
                Points = Functions.GetBonus(e.Transaction.amount),
                RefererPoint = Functions.GetBonus(e.Transaction.amount, false),
                PaymentType = e.Transaction.PaymentType(),
                AdditionalInformation = e.Transaction.AdditionalInformation(),
                DateCreated = DateTime.Now
            };
            _context.Add(Transaction);

            if(!await _context.SaveAll()){
                throw new Exception("Failed to save transaction");
            }

        }
    }
}