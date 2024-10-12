using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmail(string email);
        Task UpdateUserAsync(ApplicationUser user);

        Task<UserDTO> GetUserById(Guid id);
        Task<ApplicationUser> GetCurrentUserById();
    }
}
