using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Payment
{
    public class PaymentReturnDto
    {
        public Guid? PaymentId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentRefId { get; set; }
        public double? PaymentAmount { get; set; }
        public string? Signature { get; set; }
    }
}
