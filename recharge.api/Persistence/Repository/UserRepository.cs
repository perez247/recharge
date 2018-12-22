using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using recharge.api.Controllers.HttpResource.HttpResponseResource;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;

namespace recharge.api.Persistence.Repository 
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;
        private readonly ITransactionRepository _trans;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dbContext, ITransactionRepository trans, IMapper mapper)
        {
            _dbContext = dbContext;
            _trans = trans;
            _mapper = mapper;
        }

        public async Task<UserResponseResource> GetUser(string UserId) {

            var user = await _dbContext.Users
                        // .Include(u => u.UserTransactions)
                        .SingleOrDefaultAsync(u => u.Id.ToString() == UserId);

            if(user == null)
                throw new Exception("Server Error");
            
            var userResponseResource = _mapper.Map<UserResponseResource>(user);
            userResponseResource.Point = await _trans.GetUsersPoint(UserId);
            
            return userResponseResource;
        }

        public async Task<Decimal> UsersPoint(string UserId) {
            return await _trans.GetUsersPoint(UserId);
        }

        public async Task<UserResponseResource> UserPointAndCards(string UserId) {
            var user = await _dbContext.Users
                        .Include(u => u.Cards)
                        .SingleOrDefaultAsync(u => u.Id.ToString() == UserId);

            if(user == null)
                throw new Exception("Server Error");
            
            var userResponseResource = _mapper.Map<UserResponseResource>(user);
            userResponseResource.Point = await _trans.GetUsersPoint(UserId);
            
            return userResponseResource;
        }
    }
}