using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Dtos;
using recharge.api.Core.Models;

namespace recharge.api.Core.Interfaces
{
    public interface IAuthRepository
    {
        event EventHandler<UserEventArgs> userRegistered;
        Task<User> Register(User user, string pin, string referer = null);
        Task<User> Login(string username, string pin);
        Task<User> LoginWithAllData(string username, string pin);
        Task<bool> UserExists(string username);
        Task<string> GenerateSMSCode(User user, string Number);
        Task<bool> VerifyPhone(VerifyCodeDto verifyCodeDto);

    }
}