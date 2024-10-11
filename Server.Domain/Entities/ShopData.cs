using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class ShopData : BaseEntity
    {
        public string? TaxNumber { get; set; }
        public string? CardNumber { get; set; }
        public string? CardName { get; set; }
        public CardProviderEnum CardProvider { get; set; }

        // Foreign Keys
        public Guid UserId { get; set; }

        // Navigation Properties
        public virtual ApplicationUser User { get; set; }
    }
}
