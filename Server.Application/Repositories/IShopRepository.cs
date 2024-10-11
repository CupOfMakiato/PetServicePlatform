using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Repositories
{
    public interface IShopRepository
    {
        //INSTRUCTOR
        Task AddAsync(ShopData shopData);
        Task AddShopAsync(ShopData shopData);
        Task<ShopData> GetShopByIdAsync(Guid userId);
        Task<ApplicationUser> GetShopROLEByIdAsync(Guid shopId);
        Task<List<ApplicationUser>> GetAllShopAsync();
        Task<List<ApplicationUser>> GetPendingShopAsync();
        Task UpdateAsync(ShopData shopData);
        //USER
        Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(ApplicationUser user);
    }
}
