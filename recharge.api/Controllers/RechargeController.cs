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
using recharge.api.Helpers.Functions;

namespace recharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeController : ControllerBase 
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _repo;
        private readonly IAuthRepository _auth;
        private readonly IConfiguration _config;
        private readonly IPaymentRepository _payment;
        private readonly ITransactionRepository _transaction;
        private readonly IUserRepository _user;

        public RechargeController(
            IMapper mapper, 
            IDataRepository repo, 
            IAuthRepository auth,
            IConfiguration config,
            IPaymentRepository payment,
            ITransactionRepository transaction,
            IUserRepository user
            )
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _payment = payment;
            _transaction = transaction;
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> getUserPoint() {
            return Ok(await _user.UsersPoint(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        [HttpGet("with-card")]
        public async Task<IActionResult> getUserWithCard() {
            return Ok(await _user.UserPointAndCards(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        [HttpPost("mobile")]
        public async Task<IActionResult> rechargeMobile(MobileRechargeRequestResourse mobileRechargeRequestResource)
        {
            var user = await _auth.LoginWithAllData(User.FindFirst(ClaimTypes.NameIdentifier).Value, mobileRechargeRequestResource.Payment.Pin);

            if (user == null)
                return BadRequest("Incorrect Secret Pin");

            // _payment.transactionMade += _transaction.RecordTransaction;

            user = _payment.ProcessDatabasePayment(mobileRechargeRequestResource, user);

            //top up users phone
            //checked if it worked else
            //rollback all operations

            if(!await _repo.SaveAll()){
                //return users money
            }


            return Ok(new {user = _mapper.Map<User, UserResponseResource>(user), token = TokenFunctions.generateUserToken(user,_config, true)});
        //start with the card
        }
    }
}