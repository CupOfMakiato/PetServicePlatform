using Microsoft.Extensions.Caching.Memory;
using Server.Contracts.DTO.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TemporaryStoreService : ITemporaryStoreService
{
    private readonly IMemoryCache _cache;

    public TemporaryStoreService(IMemoryCache cache)
    {
        _cache = cache;
    }
    public async Task StoreShopRegistrationAsync(Guid userId, ShopRegisterDTO registrationDto, TimeSpan expiration)
    {
        _cache.Set(userId, registrationDto, expiration);
        await Task.CompletedTask;
    }

    public async Task<ShopRegisterDTO> GetShopRegistrationAsync(Guid userId)
    {
        _cache.TryGetValue(userId, out ShopRegisterDTO registrationDto);
        return await Task.FromResult(registrationDto);
    }
}
