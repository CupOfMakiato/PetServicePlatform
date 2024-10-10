using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public double TotalAmount { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<BillDetail> BillDetail { get; set; }
    }
}
