using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.RequestAndResponse.Service
{
    public class CreateApproveOrRejectRequest
    {
        public Guid Id { get; set; }
        public bool IsApproved { get; set; }
        public string? Reason { get; set; }
    }
}
