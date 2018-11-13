using System;

namespace recharge.Api.Dtos
{
    public class UserToReturnDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }

        public string Code { get; set; }
    }
}