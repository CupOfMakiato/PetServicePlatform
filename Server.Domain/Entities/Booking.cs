using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid User_Id { get; set; }
        public Guid Service_Id { get; set; }

        //Relationship
        public virtual ApplicationUser? User { get; set; }
        public virtual Service? Service { get; set; }
    }
}
