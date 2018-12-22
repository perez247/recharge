using System;

namespace recharge.api.Core.Models
{
    public class AppTransaction
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public string Other { get; set; }
        public Decimal Amount { get; set; }
    }
}