using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Entities.UserEntity.Command.SignUp;
using Domain.ValueObjects;
using Application.Entities.UserEntity.Command.GeneratePhoneToken;
using System;

namespace Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
             _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignUpResult> SignUp(User user, string Password, string referersPhoneNumber) {

            user.Referer = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == referersPhoneNumber);

            // throw new Exception(user.Referer.Id.ToString());

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
                return new SignUpResult() { Errors = null, User = user, 
                ConfirmPhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber) };

            return new SignUpResult() {
                Errors = string.Join(',', result.Errors.Select(x=>x.Description)),
                User = null
            };
        }

        public async Task<User> SignIn(string userNameOrEmail, string Password) {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => 
                    u.Email.ToUpper() == userNameOrEmail.ToUpper() ||
                    u.UserName.ToUpper() == userNameOrEmail.ToUpper()
                    );

            if (user == null)
                return null;

            var login = await _userManager.CheckPasswordAsync(user, Password);

            if (!login)
                return null;

            return user;
        }

        public async Task<PhoneTokenGenerated> GeneratePhoneToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;
            
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            return new PhoneTokenGenerated() {
                Token = token,
                User = user
            };
        }

        public async Task<bool> UniquePhoneNumber(string Value)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == Value);

            return user == null;
        }
    }
}