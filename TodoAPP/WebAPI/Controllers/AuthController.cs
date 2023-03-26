using Business.Abstract;
using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.success)
            {
                return BadRequest(new ErrorResult(userToLogin.message));
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.success)
            {
                return Ok(new SuccessDataResult<AccessToken>(result.Data));
            }

            return BadRequest(new ErrorResult(result.message));
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Username);
            if (!userExists.success)
            {
                return BadRequest(new ErrorResult(userExists.message));
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.success)
            {
                return Ok(new SuccessDataResult<AccessToken>(result.Data));
            }

            return BadRequest(new ErrorResult(result.message));
        }
        [HttpGet("claims")]
        public IDataResult<List<OperationClaim>> GetClaim(int id)
        {
            var user = _userService.GetUserById(id);
            return new SuccessDataResult<List<OperationClaim>>(_userService.GetClaims(user));
        }

    }
}
