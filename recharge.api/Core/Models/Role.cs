using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace recharge.api.Core.Models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; private set; }

        public Role()
        {
            UserRoles = new List<UserRole>();
        }
    }
}