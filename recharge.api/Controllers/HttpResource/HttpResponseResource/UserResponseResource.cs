using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using recharge.api.Core.Models;

namespace recharge.api.Controllers.HttpResource.HttpResponseResource
{
    public class UserResponseResource
    {
        // public ICollection<UserRole> UserRoles { get; private set; }
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }

        public string Code { get; set; }
        public PointResponseResource Point { get; set; }
        public  ICollection<CardResponseResource> Cards { get; private set; }

        public UserResponseResource()
        {
            Cards = new Collection<CardResponseResource>();
        }
    }
}