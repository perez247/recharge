namespace recharge.api.Core.Models
{
    public class VerifyUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}