using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class UserServiceDTO
    {
        public UserDTO CreatedByUser { get; set; }
    }
}
