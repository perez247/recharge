using Microsoft.AspNetCore.Mvc;
using recharge.api.Dtos;
using recharge.api.Helpers.ThirdParty;
using recharge.api.Data.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace recharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private readonly IDataRepository _repo;
        public WithdrawController(IAuthRepository auth, IDataRepository repo)
        {
            _repo = repo;
            _auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> MakeWithdraw(WithdrawDto withdrawDto)
        {

            var user = await _auth.LoginWithAllData(User.FindFirst(ClaimTypes.Name).Value, withdrawDto.Pin);

            if (user == null)
                return Unauthorized();
                
            if(!(user.Point.Points >= withdrawDto.Amount))
                return BadRequest("Insufficient Points");

            if (!CardPayment.PayClient(withdrawDto))
                return BadRequest("Transaction failed, please try again later");

            _repo.Update(user);

            user.Point.Points -= withdrawDto.Amount;
            if(!await _repo.SaveAll())
                return BadRequest("Failed to save transaction");

            return NoContent();
        }
    }
}