using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using recharge.Api.Data.EventArgTypes;
using recharge.Api.Dtos;
using recharge.Api.models;

namespace recharge.Api.Data.Interfaces
{
    public interface IAuthRepository
    {
        event EventHandler<UserEventArgs> userRegistered;
        Task<User> Register(User user, string pin);
        Task<User> Login(string username, string pin);
        Task<bool> UserExists(string username);
        Task<string> GenerateSMSCode(User user, string Number);
        Task<bool> VerifyPhone(VerifyCodeDto verifyCodeDto);

    }
}