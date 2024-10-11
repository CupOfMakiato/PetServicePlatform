using Microsoft.AspNetCore.Http;
using Server.Application.Enum;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Contracts.DTO.Email;
using Server.Contracts.DTO.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class ShopService : IShopService
    {
        private readonly IUserRepository? _userRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopService(IUserRepository? userRepository, IShopRepository shopRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _shopRepository = shopRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ApproveShopAsync(Guid userID)
        {
            var user = await _shopRepository.GetUserByIdAsync(userID);
            if (user == null)
            {
                throw new Exception("User not found !");
            }

            if (user.Status == UserStatus.Active || user.Status == UserStatus.Rejected)
            {
                throw new Exception("User has already been active or rejected !");
            }

            user.IsVerified = true;
            user.Status = UserStatus.Active; // Using the enum value
            await _userRepository.UpdateAsync(user);

            await _emailService.SendApprovalEmailAsync(user.Email);
        }

        public async Task RejectInstructorAsync(ApproveRejectShopDTO rejectDto)
        {
            var user = await _shopRepository.GetUserByIdAsync(rejectDto.ShopId);
            if (user == null)
            {
                throw new Exception("User not found !");
            }

            if (user.Status == UserStatus.Active || user.Status == UserStatus.Rejected)
            {
                throw new Exception("User has already been approved or rejected !");
            }

            user.Status = UserStatus.Rejected; // Using the enum value
            await _userRepository.UpdateAsync(user);

            await _emailService.SendRejectionEmailAsync(user.Email, rejectDto.Reason);
        }

        public async Task<string> ChangeStatusShop(ContentEmailDTO contentEmailDto, Guid id)
        { 
            var user = await _shopRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return "User not found !";
            }

            if (user.Status == UserStatus.Pending)
            {
                return "This user is in pending status !";
            }

            if (user.Status == UserStatus.Active)
            {
                await _emailService.SendDeactiveEmailAsync(user.Email, contentEmailDto.content);
                user.Status = UserStatus.Inactive;
            }
            else if (user.Status == UserStatus.Inactive)
            {
                await _emailService.SendActiveEmailAsync(user.Email);
                user.Status = UserStatus.Active;
            }
            else
            {
                return "Invalid status change request";
            }

            await _shopRepository.UpdateUserAsync(user);
            return "Status updated successfully";
        }

        public async Task<List<PendingShopDTO>> GetPendingShopsAsync()
        {
            var pendingInstructors = await _shopRepository.GetPendingShopAsync();
            return pendingInstructors.Select(instructor => new PendingShopDTO
            {
                UserId = instructor.Id,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Status = instructor.Status,
            }).ToList();
        }
    }
}
