using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Booking
{
    public class AddBookingDto
    {
        public Guid ServiceId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public OptionPay OptionPay { get; set; }
    }
}
