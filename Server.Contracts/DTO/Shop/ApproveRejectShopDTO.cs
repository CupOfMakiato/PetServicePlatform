using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Shop
{
    public class ApproveRejectShopDTO
    {
        public Guid ShopId { get; set; }
        public string Reason { get; set; }
    }
}
