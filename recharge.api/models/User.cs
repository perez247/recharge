using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace recharge.Api.models
{
    public class User : IdentityUser<Guid>
    {
        // public override string PhoneNumber { get; set; }
        public DateTime Expires { get; set; }
        // public string UserId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}