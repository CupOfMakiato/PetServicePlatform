using Server.Contracts.DTO.User;
using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Shop
{
    public class ShopRegisterDTO : UserRegistrationDTO
    {
        public string TaxNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public CardProviderEnum CardProvider { get; set; }
    }
}
