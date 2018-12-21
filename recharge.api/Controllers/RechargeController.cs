using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recharge.api.Dtos;
using recharge.api.Dtos.Payments;
using recharge.api.Helpers.ThirdParty;
using recharge.api.Core.Models;
using recharge.api.Core.Interfaces;
using recharge.api.Helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using recharge.api.Controllers.HttpResource.HttpResponseResource;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using Microsoft.Extensions.Configuration;

namespace recharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeController : ControllerBase 
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IDataRepository _repo;
        private readonly IAuthRepository _auth;
        private readonly IConfiguration _config;
        private readonly IPaymentRepository _payment;
        private readonly IPointRepository _point;
        private readonly ITransactionRepository _transaction;

        public RechargeController(
            UserManager<User> userManager, 
            IMapper mapper, 
            IDataRepository repo, 
            IAuthRepository auth,
            IConfiguration config,
            IPaymentRepository payment,
            IPointRepository point,
            ITransactionRepository transaction
            )
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _payment = payment;
            _point = point;
            _transaction = transaction;
        }

        [HttpGet]
        public async Task<IActionResult> getUser() {
            var user = await _userManager.Users
                            .Include(u => u.Cards).Include(u => u.Point)
                            .FirstOrDefaultAsync(u => u.Id.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(_mapper.Map<UserResponseResource>(user));
        }

        [HttpPost("mobile")]
        public async Task<IActionResult> rechargeMobile(MobileRechargeRequestResourse mobileRechargeRequestResource)
        {
            var user = await _auth.LoginWithAllData(User.FindFirst(ClaimTypes.NameIdentifier).Value, mobileRechargeRequestResource.Payment.Pin);

            if (user == null)
                return Unauthorized();

            _payment.transactionMade += _transaction.RecordTransaction;

            user = _payment.ProcessDatabasePayment(mobileRechargeRequestResource, user);

            //top up users phone
            //checked if it worked else
            //rollback all operations

            if(!await _repo.SaveAll()){
                //return users money
            }


            return Ok(new {user = _mapper.Map<User, UserResponseResource>(user), token = Functions.generateUserToken(user,_config, true)});
        //start with the card
        }
    }
}