﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.User
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
