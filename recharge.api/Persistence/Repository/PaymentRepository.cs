using System;
using System.Linq;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers.ThirdParty;

namespace recharge.api.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMapper _mapper;

        public PaymentRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void ValidatePayementDetail(MobileRechargeRequestResourse mobileRechargeRequestResource, User user) {
            if (user.Point.Points < mobileRechargeRequestResource.Payment.Point)
                throw new Exception("Insufficient Points");

            if (mobileRechargeRequestResource.amount != (mobileRechargeRequestResource.Payment.Point + mobileRechargeRequestResource.Payment.CardAmount))
                throw new Exception("Invalid amount requested");
        }

        public User ProcessDatabasePayment(MobileRechargeRequestResourse mobileRechargeRequestResource, User user) {

            ValidatePayementDetail(mobileRechargeRequestResource, user);

            if (mobileRechargeRequestResource.Payment.CardAmount > 0)
            {
                Card card = user.Cards.SingleOrDefault(c => c.Id.ToString() == mobileRechargeRequestResource.Payment.CardId);
                CardRequestResource cardRequestResource = (card != null) ? _mapper.Map<CardRequestResource>(card) : _mapper.Map<CardRequestResource>(mobileRechargeRequestResource.Payment.NewCard);
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
                        // return Ok(_mapper.Map<Card>(mobileRechargeRequestResource.Payment.NewCard));
                    }
                }

            }

            if (mobileRechargeRequestResource.Payment.Point > 0)
            {
                user.Point.Points = user.Point.Points - (Decimal)mobileRechargeRequestResource.Payment.Point;
            }

            user.Point.Points += Decimal.Round(mobileRechargeRequestResource.amount * 0.05m, 2, MidpointRounding.AwayFromZero);
            user.Expires = DateTime.Now.AddDays(61);

            return user;

        }
    }
}