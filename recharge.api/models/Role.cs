using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace recharge.api.models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}