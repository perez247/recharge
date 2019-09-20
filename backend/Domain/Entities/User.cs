using System;
using System.Collections.Generic;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User Referer { get; set; }
        public Guid? RefererId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }
}