using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using recharge.Api.models;

namespace recharge.Api.Helpers.CustomIdentityValidators
{
    public class UniquePhoneNumber<TUser> : IUserValidator<TUser> where TUser : User
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var userFromRepo = await manager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);
            if(userFromRepo == null)
                return IdentityResult.Success;

            return IdentityResult.Failed(
                    new IdentityError() 
                    { Code = "Unique Number", Description = "Phone Number has already been taken"}
                );
        }
    }
}
