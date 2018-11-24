using System;
using recharge.api.Dtos.Payments;

namespace recharge.api.Helpers.ThirdParty
{
    public static class CardPayment
    {
        public static Boolean Process(CardDto card) {
            return true;
        }
    }
}