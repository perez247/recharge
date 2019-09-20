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
            UserId = User.FindFirst(TokenFunctions.claims.userId.ToString()).Value;
        }

        public void SetPhoneTokenCreatedDate(ClaimsPrincipal User) {
            
            
            if(!User.HasClaim(c => c.Type == TokenFunctions.claims.mobileExpiry.ToString()))
                return;

            DateTime time;
            if(!DateTime.TryParse(User.FindFirst(TokenFunctions.claims.mobileExpiry.ToString()).Value, out time))
                return;

            PhoneTokenCreatedDate = time;
        }
    }
    
}