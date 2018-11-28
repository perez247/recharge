using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recharge.api.Data.Interfaces;
using recharge.api.Helpers;
using recharge.api.models;
using System.Security.Claims;
using AutoMapper;
using recharge.api.Dtos;

namespace recharge.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly SignInManager<User> _singInManager;
        private readonly IMapper _mapper;
        public HomeController(IDataRepository repo, SignInManager<User> singInManager, IMapper mapper)
        {
            _mapper = mapper;
            _singInManager = singInManager;
            _repo = repo;

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPoints(string userId)
        {
            if (!Functions.IsOwnerOfAccount(userId, User))
                return Unauthorized();

            var point = await _repo.GetUserPoint(userId);
            if (point == null)
                return BadRequest();

            return Ok(_mapper.Map<PointToReturnDto>(point));
        }
    }
}