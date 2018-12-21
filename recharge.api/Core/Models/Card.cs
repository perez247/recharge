using System;
using System.ComponentModel.DataAnnotations;
using recharge.api.Core.Models;

namespace recharge.api.Core.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        [StringLength(255)]
        public string CardNumber { get; set; }

        [StringLength(255)]
        public string CardHolderName { get; set; }

        [StringLength(5)]
        public string CVVNumber { get; set; }

        [StringLength(2)]
        public string ExpiryMonth { get; set; }

        [StringLength(4)]
        public string ExpiryYear { get; set; }
    }
}