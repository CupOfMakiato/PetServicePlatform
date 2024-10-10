using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class BillDetail : BaseEntity
    {
        public Guid BillId { get; set; }
        public Guid ServiceId { get; set; }
        public double Price { get; set; }
        public Payment Payment { get; set; }
        public Service Service { get; set; }
    }
}
