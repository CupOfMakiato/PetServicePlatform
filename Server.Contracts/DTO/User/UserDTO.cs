using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.User
{
    public class UserDTO
    {

        public string FullName { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }

        public string? RefreshToken { get; set; }

    }
}
