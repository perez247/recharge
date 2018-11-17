using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using recharge.api.models;

namespace recharge.Api.models
{
    public class User : IdentityUser<Guid>
    {
        // public override string PhoneNumber { get; set; }
        public Guid? RefererId { get; set; }
        public User Referer { get; set; }
        public DateTime Expires { get; set; }
        // public string UserId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public  ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}