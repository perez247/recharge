using System;
using recharge.api.Core.Models;

namespace recharge.api.Core.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVVNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}