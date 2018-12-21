using System;
using recharge.api.Core.Models;

namespace recharge.api.Helpers.CustomDataTypes.EventArgTypes
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}