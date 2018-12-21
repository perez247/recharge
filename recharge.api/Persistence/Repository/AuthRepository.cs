using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Core.Interfaces;
using recharge.api.Dtos;
using recharge.api.Core.Models;

namespace recharge.api.Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<User> _userManager;
        public event EventHandler<UserEventArgs> userRegistered;
        public AuthRepository(DataContext dbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;

        }

        protected virtual void OnUserRegistered(User user) {
            if(userRegistered != null)
                userRegistered(this, new UserEventArgs() {User = user});
        }

        public Task<string> GenerateSMSCode(User user, string number)
        {
            return _userManager.GenerateChangePhoneNumberTokenAsync(user, number);
        }

        public async Task<User> Login(string usernameOrPhone, string pin)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == usernameOrPhone.ToUpper() || u.PhoneNumber == usernameOrPhone);
            if(user == null)
                return null;
            
            if(await _userManager.CheckPasswordAsync(user, pin))
                return user;

            return null;
        }

        public async Task<User> LoginWithAllData(string userId, string pin)
        {
            var user = await _userManager.Users
                            .Include(p => p.Point)
                            .Include(c => c.Cards)
                            .Include(u => u.Referer)
                                .ThenInclude(p => p.Point)
                            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            
            if(user == null)
                return null;
            
            if(await _userManager.CheckPasswordAsync(user, pin))
                return user;

            return null;
        }

        public async Task<User> Register(User user, string pin, string referer = null)
        {
            user.Referer = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == referer.ToUpper() || u.PhoneNumber == referer );
            
            var result  = await _userManager.CreateAsync(user, pin);
            

            if(!result.Succeeded){
                throw new Exception(String.Join("\n", result.Errors.Select(x=>x.Description)));
            }

            OnUserRegistered(user);
            return user;
        }

        public async Task<bool> UserExists(string usernameOrPhone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == usernameOrPhone.ToUpper() || u.PhoneNumber == usernameOrPhone);
            if(user == null)
                return false;
            return true;
        }

        public async Task<bool> VerifyPhone(VerifyCodeDto verifyCodeDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == verifyCodeDto.Id);

            if(user == null)
                return false;

            if(!await _userManager.IsPhoneNumberConfirmedAsync(user) && 
                await _userManager.VerifyChangePhoneNumberTokenAsync(user, verifyCodeDto.Code, user.PhoneNumber) 
                ){
               user.PhoneNumberConfirmed = true;
            //    var r = await _userManager.UpdateAsync(user);
                _dbContext.Users.Update(user);
               return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

    }
}