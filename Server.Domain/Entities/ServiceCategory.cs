using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class ServiceCategory
    {
        public Guid Service_Id { get; set; }
        public Guid Category_Id { get; set; }

        //Relationship
    }
}
