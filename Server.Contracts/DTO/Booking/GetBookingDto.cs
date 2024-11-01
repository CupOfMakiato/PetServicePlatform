using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Booking
{
    public class GetBookingDto
    {
        public string UserBooking {  get; set; }
        public string ServiceBooking { get; set; }
        public DateTime TimeBooking { get; set; }
        public string StatusBooking { get; set; }
    }
}
