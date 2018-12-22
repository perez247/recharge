using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using recharge.api.Controllers.HttpResource.HttpResponseResource.Transactions;
using recharge.api.Core.Interfaces;

namespace recharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransController : ControllerBase
    {
        private readonly ITransactionRepository _trans;
        private readonly IMapper _mapper;

        public TransController(ITransactionRepository trans, IMapper mapper)
        {
            _trans = trans;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult UsersTransaction() {
           var transaction = _trans.GetUserTransaction(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(_mapper.Map<IEnumerable<UserTransactionResponseResource>>(transaction));
            // return Ok("200");
        }

        [HttpGet("other")]
        public IActionResult OtherTransaction() {
           var transaction = _trans.GetRefererTransaction(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(_mapper.Map<IEnumerable<UserTransactionResponseResource>>(transaction));
            // return Ok("200");
        }
    }
}