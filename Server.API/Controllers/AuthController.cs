using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Auth;
using Server.Contracts.DTO.User;
using Server.Domain.Entities;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PasswordService _passwordService;
        private readonly IAuthService _authService;

        public AuthController(PasswordService passwordService, IAuthService authService)
        {
            _passwordService = passwordService;
            _authService = authService;
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDTO);
                Response.Cookies.Append("refreshToken", token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/",
                    SameSite = SameSiteMode.Strict,
                });
                return Ok(new Result<object>
                {
                    Error = 0,
                    Message = "Success",
                    Data = new
                    {
                        TokenType = "Bearer",
                        AccessToken = token.AccessToken
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            try
            {
                var checkRefeshToken = await _authService.RefreshToken(token);
                Response.Cookies.Append("refreshToken", checkRefeshToken.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/",
                    SameSite = SameSiteMode.Strict,
                });
                return Ok(new Result<object>
                {
                    Error = 0,
                    Message = "Success",
                    Data = new
                    {
                        TokenType = "Bearer",
                        AccessToken = checkRefeshToken.AccessToken
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("user/logout")]
        public async Task<IActionResult> Logout([FromBody] Guid userId)
        {
            Response.Cookies.Delete("refreshToken");
            await _authService.DeleteRefreshToken(userId);
            return Ok(new Result<object>
            {
                Error = 0,
                Message = "Logout Successfully",
                Data = null
            });
        }

        [HttpPost("user/register/user")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRegistrationDTO userRegistrationDto)
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "You are already logged in and cannot register again." });
            }

            try
            {
                await _authService.RegisterUserAsync(userRegistrationDto);
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
