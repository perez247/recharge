using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recharge.api.Dtos;
using recharge.api.Dtos.Payments;
using recharge.api.Helpers.ThirdParty;
using recharge.api.models;
using recharge.api.Data.Interfaces;
using recharge.api.Helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public RechargeController(UserManager<User> userManager, IMapper mapper, IDataRepository repo, IAuthRepository auth)
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("mobile")]
        public async Task<IActionResult> rechargeMobile(MobileRechargeDto mobileRechargeDto)
        {

            var user = await _auth.LoginWithAllData(User.FindFirst(ClaimTypes.NameIdentifier).Value, mobileRechargeDto.Payment.Pin);

            if (user == null)
                return Unauthorized();

            if (user.Point.Points < mobileRechargeDto.Payment.Point)
                return BadRequest("Insufficient Points");

            if (mobileRechargeDto.amount != (mobileRechargeDto.Payment.Point + mobileRechargeDto.Payment.CardAmount))
                return BadRequest("Invalid amount requested");

            _repo.BeginTransaction();
            _repo.Update(user);

            try
            {
                if (mobileRechargeDto.Payment.CardAmount > 0)
                {
                    Card card = user.Cards.SingleOrDefault(c => c.Id.ToString() == mobileRechargeDto.Payment.CardId);
                    CardDto cardDto = (card != null) ? _mapper.Map<CardDto>(card) : _mapper.Map<CardDto>(mobileRechargeDto.Payment.NewCard);
                    var result = cardDto.Validate();

                    if (result != null)
                        return BadRequest(result);

                    // All set to take money from user account
                    if (!CardPayment.Process(cardDto))
                        return BadRequest("Failed to perform card transaction");

                    if (mobileRechargeDto.Payment.SaveCard)
                    {
                        var newCard = user.Cards.FirstOrDefault(c => c.CardNumber == mobileRechargeDto.Payment.NewCard.CardNumber);
                        if (newCard == null)
                        {
                            user.Cards.Add(mobileRechargeDto.Payment.NewCard);
                        }
                    }

                }

                if (mobileRechargeDto.Payment.Point > 0)
                {
                    user.Point.Points = user.Point.Points - (Decimal)mobileRechargeDto.Payment.Point;
                }

                //top up users phone
                //checked if it worked else
                //rollback all operations

                user.Point.Points += Decimal.Round(mobileRechargeDto.amount * 0.05m, 2, MidpointRounding.AwayFromZero);

                if(!await _repo.SaveAll()){
                    //return users money
                }
                _repo.Commit();

                return NoContent();

            }
            catch (Exception)
            {
                //
                _repo.RollBack();
                return BadRequest("Unknown error occured");
            }

            //start with the card
        }
    }
}