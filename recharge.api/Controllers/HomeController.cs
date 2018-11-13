using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recharge.Api.Data.Interfaces;
using recharge.Api.Helpers;
using recharge.Api.models;
using System.Security.Claims;

namespace recharge.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly SignInManager<User> _singInManager;
        public HomeController(IDataRepository repo, SignInManager<User> singInManager)
        {
            _singInManager = singInManager;
            _repo = repo;

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPoints(string userId)
        {
            if(!Functions.IsOwnerOfAccount(userId, User))
                return Unauthorized();
            
            var point = await _repo.GetUserPoint(userId);
            if(point == null)
                return BadRequest();

            return Ok(point);
        }
    }
}