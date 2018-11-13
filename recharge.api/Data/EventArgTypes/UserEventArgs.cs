using System;
using recharge.Api.models;

namespace recharge.Api.Data.EventArgTypes
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}