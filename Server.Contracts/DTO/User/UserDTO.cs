using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }

        public string? RefreshToken { get; set; }

        public double? Balance { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
