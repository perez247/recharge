using System.Net;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.GeneratePhoneToken;
using Application.Entities.UserEntity.Command.SignUp;
using Application.Entities.UserEntity.Query.SignIn;
using Application.Entities.UserEntity.Query.Unique;
using Application.Infrastructure.RequestResponsePipeline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Sign up a user to the platform
        /// </summary>
        /// <response code="200">Signup successfull</response>
        /// <response code="400">Signup failed, providing error message</response>
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser(SignUpCommand command) {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }

        /// <summary>
        /// Sign in a user to the platform
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Signed in user to platformt</response>
        /// <response code="404">Failed to sign up user with error message</response>
        [AllowAnonymous]
        [ProducesResponseType(typeof(SignInModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInCommand command) {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }

        /// <summary>
        /// Sign up a user to the platform
        /// </summary>
        /// <response code="200">Signup successfull</response>
        /// <response code="400">Signup failed, providing error message</response>
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [HttpGet("generate-phone-token")]
        public async Task<IActionResult> GeneratePhoneToken() {
            return Ok(await Mediator.Send(new GeneratePhoneTokenCommand(User) ));
            // return Ok(User);
        }

        [AllowAnonymous]
        [HttpGet("unique")]
        public async Task<IActionResult> UniqueField([FromQuery] UniqueCommand command) {
            return Ok(await Mediator.Send(command));
            // return Ok(User);
        }
    }
}