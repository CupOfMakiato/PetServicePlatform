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
        public Guid BookingId { get; set; }
        public string PaymentUrl { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public ApplicationUser ApplicationUser{ get; set; }
        public List<BillDetail> BillDetail { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
