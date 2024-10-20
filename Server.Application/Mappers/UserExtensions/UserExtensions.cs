﻿using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Mappers.UserExtensions
{
    public static class UserExtensions
    {
        public static UserDTO ToUserDTO(this ApplicationUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Balance = null,
            };
        }
    }
}
