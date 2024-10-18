﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Enum;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Auth;
using Server.Contracts.DTO.Shop;
using Server.Contracts.DTO.User;
using Server.Contracts.Enum;
using Server.Domain.Entities;
using System.Security.Claims;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PasswordService _passwordService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService; 
        private readonly IEmailService _emailService;

        public AuthController(PasswordService passwordService, IAuthService authService, IUserService userService, IEmailService emailService)
        {
            _passwordService = passwordService;
            _authService = authService;
            _userService = userService;
            _emailService = emailService;
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


        //Register user
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
                return Ok(new { Message = "Registration successful. Please check your email for the OTP. " });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //Register Shop
        [HttpPost("user/register/shop")]
        public async Task<IActionResult> RegisterInstructor([FromForm] ShopRegisterDTO shopRegisterDTO)
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "You are already logged in and cannot register again." });
            }
            try
            {
                await _authService.RegisterInstructorAsync(shopRegisterDTO);
                return Ok(new { Message = "Registration successful. Please check your email for the OTP." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("user/otp/verify")]
        public async Task<IActionResult> VerifyOtp(OtpVerificationDTO otpVerificationDto)
        {
            try
            {
                var isValid = await _authService.VerifyOtpAndCompleteRegistrationAsync(otpVerificationDto.Email, otpVerificationDto.Otp);
                if (!isValid)
                {
                    return BadRequest(new { Message = "Invalid OTP or OTP has expired." });
                }

                var user = await _userService.GetByEmail(otpVerificationDto.Email);
                if (user != null)
                {
                    user.IsVerified = true;

                    if (user.RoleCodeId == 3) // Role 3 is for shop
                    {
                        user.Status = UserStatus.Pending;
                        await _emailService.SendPendingEmailAsync(user.Email);
                    }
                    else
                    {
                        user.Status = UserStatus.Active;
                        await _emailService.SendApprovalEmailAsync(user.Email);
                    }

                    await _userService.UpdateUserAsync(user);
                }

                return Ok(new { Message = "Email verified successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        [HttpPost("user/password/change")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDTO changePasswordDto)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email == null)
                {
                    return Unauthorized(new { Message = "Invalid token." });
                }

                await _authService.ChangePasswordAsync(email, changePasswordDto);
                return Ok(new { Message = "Password changed successfully. Please log in again." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Requests a password reset link to be sent to the user's email.
        /// </summary>
        [HttpPost("user/password/forgot")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequestDTO forgotPasswordRequestDto)
        {
            try
            {
                await _authService.RequestPasswordResetAsync(forgotPasswordRequestDto);
                return Ok(new { Message = "Password reset link sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the user's password.
        /// </summary>
        [HttpPost("user/password/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            try
            {
                await _authService.ResetPasswordAsync(resetPasswordDto);
                return Ok(new { Message = "Password reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("providers/cards")]
        public IActionResult GetCardProviders()
        {
            var cardProviders = Enum.GetValues(typeof(CardProviderEnum))
                                    .Cast<CardProviderEnum>()
                                    .Select(e => new { Id = (int)e, Name = e.ToString() })
                                    .ToList();
            return Ok(cardProviders);
        }
    }
}
