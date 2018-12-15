using System;
using recharge.api.Core.Models;

namespace recharge.api.Core.Models
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Points { get; set; }
        public Decimal RefererPoint { get; set; }
        public string PaymentType { get; set; }
        public DateTime DateCreated { get; set; }
    }
}