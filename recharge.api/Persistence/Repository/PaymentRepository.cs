using System;
using System.Linq;
using System.Transactions;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Internal;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Helpers.Functions;
using recharge.api.Helpers.ThirdParty;

namespace recharge.api.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _trans;
        private readonly IDataRepository _repo;

        public event EventHandler<CustomTransactionEventArgs> transactionMade;

        public PaymentRepository(IMapper mapper, ITransactionRepository trans, IDataRepository repo)
        {
            _mapper = mapper;
            _trans = trans;
            _repo = repo;
        }

        protected virtual void OnTransactionMade(UserTransaction userTransaction) {
            if(transactionMade != null){
                transactionMade(this, new CustomTransactionEventArgs() {
                    UserTransaction = userTransaction
                });
            }
        }

        public async void ValidatePayementDetail(RechargeRequestResource paymentRequest, User user) {
            var Points = await _trans.GetUsersPoint(user.Id.ToString());
            if (Points < paymentRequest.Payment.Point)
                throw new Exception("Insufficient Points");

            if (paymentRequest.Amount != (paymentRequest.Payment.Point + paymentRequest.Payment.CardAmount))
                throw new Exception("Invalid amount requested");
        }

        public User ProcessDatabasePayment(RechargeRequestResource paymentRequest, User user) {

            ValidatePayementDetail(paymentRequest, user);
            var userTransaction = TransactionFunctions.GenerateUserTransaction(paymentRequest.Amount, user);

            if (paymentRequest.Payment.CardAmount > 0)
            {
                Card card = user.Cards.SingleOrDefault(c => c.Id.ToString() == paymentRequest.Payment.CardId);
                CardRequestResource cardRequestResource = (card != null) ? _mapper.Map<CardRequestResource>(card) : _mapper.Map<CardRequestResource>(paymentRequest.Payment.NewCard);
                var result = cardRequestResource.Validate();

                if (result != null)
                    throw new Exception(result);

                // All set to take money from user account
                if (!CardPayment.Process(cardRequestResource))
                    throw new Exception("Failed to perform card transaction");

                if (cardRequestResource.SaveCard)
                {
                    var newCard = user.Cards.FirstOrDefault(c => c.CardNumber == cardRequestResource.CardNumber);
                    if (newCard == null)
                    {
                        user.Cards.Add(_mapper.Map<Card>(cardRequestResource));
                    }
                }
                var newTransaction = TransactionFunctions.GenerateAppTransaction(cardRequestResource.CardNumber, (Decimal)paymentRequest.Payment.CardAmount);

                userTransaction.Transactions.Add(newTransaction);

                // _repo.SaveAll();

            }

            if (paymentRequest.Payment.Point > 0)
            {
                var pointTransaction = TransactionFunctions.GenerateAppTransaction("Points", (Decimal)paymentRequest.Payment.Point, false);

                userTransaction.Transactions.Add(pointTransaction);
            }


            _trans.RecordTransaction(userTransaction, paymentRequest);
            // _point.AddPoint(paymentRequest.Amount, user);
            // OnTransactionMade(userTransaction);

            return user;
        }
    }
}