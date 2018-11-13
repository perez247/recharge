using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using recharge.Api.models;

namespace recharge.Api.Helpers
{
    public static class Functions
    {
        public static object generateUserToken(User user, IConfiguration _config) {
                        // Create token an sent;
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("phone", user.PhoneNumber),
                new Claim("isPhoneConfirmed", user.PhoneNumberConfirmed.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            var data = new {token = tokenhandler.WriteToken(token)};

            return data;
        }

        public static bool IsOwnerOfAccount(string userId, ClaimsPrincipal User) {
            return userId == User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }

}