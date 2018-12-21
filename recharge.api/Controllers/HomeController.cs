using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recharge.api.Core.Interfaces;
using recharge.api.Helpers;
using recharge.api.Core.Models;
using System.Security.Claims;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpResponseResource;

namespace recharge.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPointRepository _point;
        private readonly SignInManager<User> _singInManager;
        private readonly IMapper _mapper;
        public HomeController(IPointRepository point, SignInManager<User> singInManager, IMapper mapper)
        {
            _mapper = mapper;
            _singInManager = singInManager;
            _point = point;

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPoints(string userId)
        {
            if (!Functions.IsOwnerOfAccount(userId, User))
                return Unauthorized();

            var point = await _point.GetUserPoint(userId);
            if (point == null)
                return BadRequest();

            return Ok(_mapper.Map<PointResponseResource>(point));
        }
    }
}