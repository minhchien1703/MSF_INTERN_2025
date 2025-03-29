using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using user_management.Data;
using user_management.Dto.RequestDto;
using user_management.Respositories.BlackListToken;
using user_management.Services.AuthenService;

namespace user_management.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        private readonly IBlackListTokenRepository _blackListTokenRepository;

        public AuthenController(IAuthenService authenService, IBlackListTokenRepository blackListToken)
        {
            _authenService = authenService;
            _blackListTokenRepository = blackListToken;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            try
            {
                var response = await _authenService.Register(registerRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Register error", Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                return Ok(_authenService.Login(loginRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is not valid!" });
            }

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var expiration = jwtToken?.ValidTo ?? DateTime.UtcNow;

            _blackListTokenRepository.AddToBlackList(token, expiration);

            return Ok(new { message = "Logout success." });
        }



    }
}
