using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class UserRole
    {
        public Guid User_Id { get; set; }
        public Guid Role_Id { get; set; }

        //Relationship
        public virtual Role? Role { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
