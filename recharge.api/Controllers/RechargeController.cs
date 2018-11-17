using Microsoft.AspNetCore.Mvc;
using recharge.api.Dtos;

namespace recharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeController : ControllerBase
    {
        public RechargeController()
        {
            
        }

        [HttpPost("mobile")]
        public IActionResult rechargeMobile(MobileRechargeDto mobileRechargeDto) {
            return Ok(mobileRechargeDto);
        }
    }
}