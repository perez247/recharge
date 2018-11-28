using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using recharge.api.Data.Interfaces;
using recharge.api.Dtos;
using recharge.api.Helpers;
using recharge.api.models;

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
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = await _auth.Login(loginUserDto.Username, loginUserDto.Pin);
            if(user == null)
                return Unauthorized();

            var userToReturn = _mapper.Map<UserToReturnDto>(user);
            
            return Ok(new {token =  Functions.generateUserToken(user,_config), user = userToReturn });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            User newUser = _mapper.Map<User>(registerUserDto);

            _auth.userRegistered  += _repo.CreatePoint;

            var user = await _auth.Register(newUser, registerUserDto.Pin);

            var userToReturnDto = _mapper.Map<UserToReturnDto>(user);

            userToReturnDto.Code = await _auth.GenerateSMSCode(user, user.PhoneNumber);
            // _repo.SaveCode(userToReturnDto.Id, userToReturnDto.Code);
            // if(await _repo.SaveAll())
            // return CreatedAtRoute(nameof(GetUser), new {UserId = newUser.Id }, userToReturnDto);
            return Ok(new {user = userToReturnDto, token = Functions.generateUserToken(user,_config, true)});
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
            var userFromRepo = await _userManager.FindByIdAsync(userId);

            if(userFromRepo == null) {
                return BadRequest();
            }

            if(await _userManager.IsPhoneNumberConfirmedAsync(userFromRepo))
                return BadRequest("Your Phone has already been confirmed");

            //instead of sending the code it will be sent to the phone directly and an Ok will be send back
            return Ok(await _auth.GenerateSMSCode(userFromRepo, userFromRepo.PhoneNumber));
        }



        // [HttpGet("resendcode/{userId}")]
        // public async Task<IActionResult> sendCode(string userId) {
        //     var User = await _userManager.FindByIdAsync(userId);
        // }
    }
}