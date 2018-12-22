using System;
using System.Threading.Tasks;
using recharge.api.Controllers.HttpResource.HttpResponseResource;

namespace recharge.api.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponseResource> GetUser(string UserId);
        Task<Decimal> UsersPoint(string UserId);
        Task<UserResponseResource> UserPointAndCards(string UserId);
    }
}