using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class UserService 
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
    }
}
