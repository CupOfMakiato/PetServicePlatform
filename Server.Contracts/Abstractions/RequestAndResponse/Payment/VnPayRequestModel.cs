using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.RequestAndResponse.Payment
{
    public class VnPayRequestModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
