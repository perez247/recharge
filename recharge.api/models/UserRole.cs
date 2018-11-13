using System;
using Microsoft.AspNetCore.Identity;

namespace recharge.Api.models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}