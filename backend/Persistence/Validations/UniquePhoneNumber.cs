using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Validations
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