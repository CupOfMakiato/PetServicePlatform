using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ShopId { get; set; }
        public Guid ServiceId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public string OptionPay { get; set; }
        public bool IsPayment { get; set; }
        public bool IsCheckIn { get; set; }
        public ApplicationUser User { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
