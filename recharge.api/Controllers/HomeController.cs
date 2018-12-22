using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recharge.api.Core.Interfaces;
using recharge.api.Helpers;
using recharge.api.Core.Models;
using System.Security.Claims;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpResponseResource;
using recharge.api.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using recharge.api.Helpers.Functions;

namespace recharge.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SignInManager<User> _singInManager;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transaction;

        public HomeController(
            SignInManager<User> singInManager, 
            IMapper mapper,
            ITransactionRepository transaction)
        {
            _mapper = mapper;
            _transaction = transaction;
            _singInManager = singInManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetPoints()
        {
            // if (!TokenFunctions.IsOwnerOfAccount(userId, User))
            //     return Unauthorized();

            var point = await _transaction.GetUsersPoint(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(point);
        }

        [AllowAnonymous]
        [HttpGet("mytransactions")]
        public IActionResult GetUserTransactions(string userId)
        {
            var value = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (value == null)
                return Unauthorized();

            var transactions = _transaction.GetUserTransaction(value);

            return Ok(transactions);
        }

    }
}