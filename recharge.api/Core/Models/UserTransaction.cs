using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using recharge.api.Core.Models;

namespace recharge.api.Core.Models
{
    public class UserTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Decimal Amount { get; set; }
        public Decimal UserPoint { get; set; }
        public Decimal Balance { get; set; }

        [StringLength(255)]
        public string AdditionalInformation { get; set; }
        public Decimal TransactionFee { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<AppTransaction> Transactions { get; set; }

        public UserTransaction()
        {
            Transactions = new List<AppTransaction>();
        }
    }
}