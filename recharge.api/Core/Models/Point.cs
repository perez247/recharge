using System;

namespace recharge.api.Core.Models
{
    public class Point
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Decimal Points { get; set; }
    }
}