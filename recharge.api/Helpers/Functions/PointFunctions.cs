using System;

namespace recharge.api.Helpers.Functions
{
    public static class PointFunctions
    {
        private static Decimal UserBonus = 0.05m;
        private static Decimal ReferersBonus = 0.05m;
        public static Decimal GetBonus(Decimal amount, Boolean isUser = true){
            return isUser ? Decimal.Round(amount * UserBonus, 2, MidpointRounding.AwayFromZero) : Decimal.Round(amount * ReferersBonus, 2, MidpointRounding.AwayFromZero);
        }
    }
}