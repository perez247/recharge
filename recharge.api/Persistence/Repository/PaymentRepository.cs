using System;
using System.Linq;
using System.Transactions;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Internal;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Helpers.ThirdParty;

namespace recharge.api.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMapper _mapper;
        private readonly IPointRepository _point;

        public event EventHandler<CustomTransactionEventArgs> transactionMade;

        public PaymentRepository(IMapper mapper, IPointRepository point)
        {
            _mapper = mapper;
            _point = point;
        }

        protected virtual void OnTransactionMade(RechargeRequestResource rechargeRequestResource, User user) {
            if(transactionMade != null){
                transactionMade(this, new CustomTransactionEventArgs() {
                    User = user,
                    Transaction = rechargeRequestResource
                });
            }
        }

        public void ValidatePayementDetail(RechargeRequestResource paymentRequest, User user) {
            if (user.Point.Points < paymentRequest.Payment.Point)
                throw new Exception("Insufficient Points");

            if (paymentRequest.amount != (paymentRequest.Payment.Point + paymentRequest.Payment.CardAmount))
                throw new Exception("Invalid amount requested");
        }

        public User ProcessDatabasePayment(RechargeRequestResource paymentRequest, User user) {

            ValidatePayementDetail(paymentRequest, user);

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

            }

            if (paymentRequest.Payment.Point > 0)
            {
                user.Point.Points = user.Point.Points - (Decimal)paymentRequest.Payment.Point;
            }

            _point.AddPoint(paymentRequest.amount, user);
            OnTransactionMade(paymentRequest, user);

            // user.Point.Points += Decimal.Round(paymentRequest.amount * 0.05m, 2, MidpointRounding.AwayFromZero);
            // user.Expires = DateTime.Now.AddDays(61);

            return user;
        }
    }
}