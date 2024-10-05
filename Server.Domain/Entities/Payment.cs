using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid User_Id { get; set; }
        public Guid Service_Id { get; set; }

        //Relationship
    }
}
