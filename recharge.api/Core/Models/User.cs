using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using recharge.api.Core.Models;

namespace recharge.api.Core.Models
{
    public class User : IdentityUser<Guid>
    {
        // public override string PhoneNumber { get; set; }
        public Guid? RefererId { get; set; }
        public User Referer { get; set; }
        public DateTime Expires { get; set; }
        // public string UserId { get; set; }
        public ICollection<UserRole> UserRoles { get; private set; }
        public Point Point { get; set; }
        public  ICollection<PaymentTransaction> PaymentTransactions { get; private set; }
        public  ICollection<Card> Cards { get; private set; }

        public User()
        {
            UserRoles = new List<UserRole>(); 
            PaymentTransactions = new List<PaymentTransaction>();
            Cards = new List<Card>();
        }
    }
}