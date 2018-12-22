using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using recharge.api.Core.Interfaces;
using recharge.api.Helpers;
using recharge.api.Core.Models;
using System.Security.Claims;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Authentication;
using recharge.api.Controllers.HttpResource.HttpResponseResource;
using recharge.api.Dtos;
using recharge.api.Helpers.Functions;

namespace recharge.api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _auth;
        private readonly IDataRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<User> userManager, 
            IMapper mapper, 
            IAuthRepository auth,
            IDataRepository repo,
            IConfiguration config
            )
        {
            _auth = auth;
            _repo = repo;
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
            
        }

        [HttpGet("{userId}", Name="GetUser")]
        public IActionResult GetUser(string userId) {
            // var user = await _userManager.FindByIdAsync(userId);

            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestResource loginRequestResource)
        {
            var user = await _auth.Login(loginRequestResource.Username, loginRequestResource.Pin);
            if(user == null)
                return Unauthorized();

            var userResponseResource = _mapper.Map<UserResponseResource>(user);
            
            return Ok(new {token =  TokenFunctions.generateUserToken(user,_config), user = userResponseResource });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestResource registerRequestResource)
        {
            User newUser = _mapper.Map<User>(registerRequestResource);

            var user = await _auth.Register(newUser, registerRequestResource.Pin, registerRequestResource.Referer);

            var userResponseResource = _mapper.Map<UserResponseResource>(user);

            userResponseResource.Code = await _auth.GenerateSMSCode(user, user.PhoneNumber);
            // _repo.SaveCode(userResponseResource.Id, userResponseResource.Code);
            // if(await _repo.SaveAll())
            // return CreatedAtRoute(nameof(GetUser), new {UserId = newUser.Id }, userResponseResource);
            return Ok(new {user = userResponseResource, token = TokenFunctions.generateUserToken(user,_config, true)});
        }

        [HttpPost("verifyphone")]
        public async Task<IActionResult> VerifyPhone(VerifyCodeDto verifyCodeDto){

            return Ok(await _auth.VerifyPhone(verifyCodeDto));
        }

        [HttpGet("exists/{name}")]
        public async Task<IActionResult> UserExists(string name){
            return Ok(await _auth.UserExists(name));
        }

        // For Dev only, normally it will send as an SMS
        [HttpGet("resendcode/{userId}")]
        public async Task<IActionResult> ResendCode(string userId) {

            if(!TokenFunctions.HasPhoneTimeElapse(User))
                return BadRequest("Kindly wait for 5 mins");

            var userFromRepo = await _userManager.FindByIdAsync(userId);

            if(userFromRepo == null) {
                return BadRequest("Bad Request");
            }

            if(await _userManager.IsPhoneNumberConfirmedAsync(userFromRepo))
                return BadRequest("Your Phone has already been confirmed");

            var userToReturnDto = _mapper.Map<UserToReturnDto>(userFromRepo);
            userToReturnDto.Code = await _auth.GenerateSMSCode(userFromRepo, userFromRepo.PhoneNumber);

            //instead of sending the code it will be sent to the phone directly and an Ok will be send back
             return Ok(new {user = userToReturnDto, token = TokenFunctions.generateUserToken(userFromRepo,_config, true)});
            // return Ok(await _auth.GenerateSMSCode(userFromRepo, userFromRepo.PhoneNumber));
        }



        // [HttpGet("resendcode/{userId}")]
        // public async Task<IActionResult> sendCode(string userId) {
        //     var User = await _userManager.FindByIdAsync(userId);
        // }
    }
}