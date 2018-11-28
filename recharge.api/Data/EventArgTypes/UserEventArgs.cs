using System;
using recharge.api.models;

namespace recharge.api.Data.EventArgTypes
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}