using System;
using recharge.api.Core.Models;

namespace recharge.api.Controllers.HttpResource.HttpResponseResource
{
    public class CardResponseResource
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
    }
}