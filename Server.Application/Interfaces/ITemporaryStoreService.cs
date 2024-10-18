using Server.Contracts.DTO.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITemporaryStoreService
{
    Task StoreShopRegistrationAsync(Guid userId, ShopRegisterDTO registrationDto, TimeSpan expiration);
    Task<ShopRegisterDTO> GetShopRegistrationAsync(Guid userId);
}
