using System;
using recharge.api.Core.Models;

namespace recharge.api.Persistence.Repository.EventArgTypes
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}