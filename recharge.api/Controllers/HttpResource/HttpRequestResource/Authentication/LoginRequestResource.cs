using System.ComponentModel.DataAnnotations;

namespace recharge.api.Controllers.HttpResource.HttpRequestResource.Authentication
{
    public class LoginRequestResource
    {
        [Required]
        public string Username { get; set; }

        [Required, StringLength(8, MinimumLength = 8, ErrorMessage="Pin lenght of 8")]
        public string Pin { get; set; }
    }
}