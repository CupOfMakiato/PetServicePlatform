using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Utils;
using Server.Contracts.DTO.User;
using Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailService _emailService;
        private readonly TokenGenerators _tokenGenerators;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RedisService _redisService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClaimsService _claimsService;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IAuthRepository authRepository, IEmailService emailService, TokenGenerators tokenGenerators, IHttpContextAccessor httpContextAccessor, RedisService redisService, ICategoryRepository categoryRepository, IClaimsService claimsService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _authRepository = authRepository;
            _emailService = emailService;
            _tokenGenerators = tokenGenerators;
            _httpContextAccessor = httpContextAccessor;
            _redisService = redisService;
            _categoryRepository = categoryRepository;
            _claimsService = claimsService;
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User is not exist!");
            }

            UserDTO userDto = new()
            {
                FullName = user.FullName,
                Email = user.Email,
            };

            return userDto;
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task<ApplicationUser> GetCurrentUserById()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
                throw new Exception("Token not found");

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                throw new Exception("Invalid token");

            var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == "id").Value);
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}
