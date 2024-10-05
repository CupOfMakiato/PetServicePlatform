using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Service_Name { get; set; }
        public decimal Price { get; set; }

        //Relationship
        public virtual ICollection<UserService> UserServices { get; set; } = new HashSet<UserService>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public virtual ICollection<ServiceCategory> ServiceCategories { get; set; } = new HashSet<ServiceCategory>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
