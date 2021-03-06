using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using recharge.api.Core.Models;

namespace recharge.api.Helpers.Functions
{
    public static class TokenFunctions
    {
            public static string generateUserToken(User user, IConfiguration _config, Boolean confirmPhone = false) {
                        // Create token an sent;
            var claims = (confirmPhone == true) ? ConfirmPhoneTime(user) : defaultClaim(user);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            // var data = new {token = tokenhandler.WriteToken(token)};

            return tokenhandler.WriteToken(token);
        }

        
        public static bool IsOwnerOfAccount(string userId, ClaimsPrincipal User) {
            
            return userId == User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        
        public static bool HasPhoneTimeElapse(ClaimsPrincipal User) {
            
            DateTime time;
            if(!DateTime.TryParse(User.FindFirst(ClaimTypes.Name).Value, out time))
                return false;

            if(!(DateTime.Now >= time))
                return false;

            return true;
        }
        
        public static string GetUserId(ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private static Claim[] defaultClaim(User user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return claims;
        }

        private static Claim[] ConfirmPhoneTime(User user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, DateTime.Now.AddMinutes(5).ToString())
            };

            return claims;
        }
    }
}