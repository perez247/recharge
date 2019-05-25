using System;
using System.Security.Claims;

namespace Application.Infrastructure.Token
{
    public class TokenCredentials
    {
        public TokenCredentials(ClaimsPrincipal user)
        {
            setTokens(user);
        }

        public string UserId { get; set; }
        public DateTime? PhoneTokenCreatedDate { get; set; }

        private void setTokens(ClaimsPrincipal User) {
            SetUserId(User);
            SetPhoneTokenCreatedDate(User);
        }

        private void SetUserId(ClaimsPrincipal User)
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void SetPhoneTokenCreatedDate(ClaimsPrincipal User) {
            
            
            if(!User.HasClaim(c => c.Type == ClaimTypes.MobilePhone))
                return;

            DateTime time;
            if(!DateTime.TryParse(User.FindFirst(ClaimTypes.MobilePhone).Value, out time))
                return;

            PhoneTokenCreatedDate = time;
        }
    }
    
}