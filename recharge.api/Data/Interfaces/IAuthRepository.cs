using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using recharge.api.Data.EventArgTypes;
using recharge.api.Dtos;
using recharge.api.models;

namespace recharge.api.Data.Interfaces
{
    public interface IAuthRepository
    {
        event EventHandler<UserEventArgs> userRegistered;
        Task<User> Register(User user, string pin);
        Task<User> Login(string username, string pin);
        Task<User> LoginWithAllData(string username, string pin);
        Task<bool> UserExists(string username);
        Task<string> GenerateSMSCode(User user, string Number);
        Task<bool> VerifyPhone(VerifyCodeDto verifyCodeDto);

    }
}