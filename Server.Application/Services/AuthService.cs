using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Utils;
using Server.Contracts.DTO.Auth;
using Server.Contracts.DTO.User;
using Server.Domain.Entities;
using Server.Infrastructure.Services;

namespace Server.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly TokenGenerators _tokenGenerators;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly RedisService _redisService;
        private readonly IOtpService _otpService;

        public AuthService(IAuthRepository authRepository, TokenGenerators tokenGenerators, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IConfiguration configuration, RedisService redisService, IOtpService otpService, IMapper mapper)
        {
            _authRepository = authRepository;
            _tokenGenerators = tokenGenerators;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _configuration = configuration;
            _redisService = redisService;
            _otpService = otpService;
            _mapper = mapper;
        }

        public async Task<Authenticator> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(loginDTO.Email);

                if (user == null)
                {
                    throw new KeyNotFoundException("Invalid email or account does not exist.");
                }

                if (!user.Active)
                {
                    throw new InvalidOperationException("Account is not activated. Please verify your email.");
                }
                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid password.");
                }

                // Generate JWT token
                var token = await GenerateJwtToken(user);
                return token;
            }
            catch (KeyNotFoundException ex)
            {
                // Handle cases where the user is not found
                throw new ApplicationException("Invalid email or account does not exist.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle cases where the account is not verified
                throw new ApplicationException("Account is not activated. Please verify your email.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle cases where the password is invalid
                throw new ApplicationException("Invalid password.", ex);
            }
            catch (Exception ex)
            {
                // General exception handling
                throw new ApplicationException("An error occurred during login.", ex);
            }
        }

        public async Task RegisterUserAsync(UserRegistrationDTO userRegistrationDto)
        {
            try
            {
                if (await _userRepository.ExistsAsync(u => u.Email == userRegistrationDto.Email))
                {
                    throw new Exception("User with this email or phone number already exists.");
                }
                var user = new ApplicationUser
                {
                    FullName = userRegistrationDto.FullName,
                    Email = userRegistrationDto.Email,
                    Password = HashPassword(userRegistrationDto.Password),
                    Balance = 0,
                    AvatarUrl = "",
                    Introduction = userRegistrationDto.Introduction,
                    Active = false,
                    IsStaff = false,
                    RoleCodeId = 2, 
                    CreationDate = DateTime.Now,
                    
                };

                await _userRepository.AddAsync(user);
/*                await _emailService.SendOtpEmailAsync(user.Email, otp);*/
            }
            catch (ArgumentNullException ex)
            {
                // Handle cases where required information is missing
                throw new ApplicationException("Missing required registration information.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle cases where an operation is invalid, such as duplicate user registration
                throw new ApplicationException("Invalid operation during user registration.", ex);
            }
            catch (Exception ex)
            {
                // General exception handling
                throw new ApplicationException("An error occurred while registering the user.", ex);
            }
        }

        public async Task<Authenticator> RefreshToken(string token)
        {
            //Check refreshToken have validate
            var checkRefreshToken = _tokenGenerators.ValidateRefreshToken(token);
            if (!checkRefreshToken)
                return null;
            //Check refreshToken in DB
            var user = await _authRepository.GetRefreshToken(token);
            if (user == null) return null;
            List<Claim> claims = new() {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.RoleCode.RoleName)
        };

            var (accessToken, refreshToken) = _tokenGenerators.GenerateTokens(claims);

            await _authRepository.UpdateRefreshToken(user.Id, refreshToken);
            return new Authenticator()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private async Task<Authenticator> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()), // Ensuring UserId claim is added
                new Claim(ClaimTypes.Email, user.Email),
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Token expiration set to 30 minutes
            signingCredentials: creds
            );

            var refreshToken = Guid.NewGuid().ToString();
            await _authRepository.UpdateRefreshToken(user.Id, refreshToken);

            return new Authenticator
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        //logout

        public async Task<bool> DeleteRefreshToken(Guid userId)
        {
            return await _authRepository.DeleteRefreshToken(userId);
        }

    }
}
