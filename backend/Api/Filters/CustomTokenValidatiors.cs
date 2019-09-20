using System.Threading.Tasks;
using Application.Infrastructure.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Filters
{
    public class CustomTokenValidatiors
    {
        public static Task IsPhoneConfirmValidator(TokenValidatedContext context)
        {
            var value = context.Principal.FindFirst(TokenFunctions.claims.isConfirmed.ToString()).Value;
            var isPhoneNumberConfirmed = bool.Parse(value);

            if ( !isPhoneNumberConfirmed ) 
            {
                context.Fail("Phone number is not confirmed");
            }

            return Task.CompletedTask;
        }
    }
}