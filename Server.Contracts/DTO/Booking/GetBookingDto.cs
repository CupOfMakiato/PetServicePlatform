using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Booking
{
    public class GetBookingDto
    {
        public Guid BookingId { get; set; }
        public Guid UserBooking {  get; set; }
        public Guid ServiceBooking { get; set; }
        public DateTime TimeBooking { get; set; }
        public string StatusBooking { get; set; }
        public bool IsPayment { get; set; }
        public bool IsCheckIn { get; set; }
    }
}
