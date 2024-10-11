using Microsoft.EntityFrameworkCore;
using Server.Application.Enum;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class ShopRepository : GenericRepository<ApplicationUser>, IShopRepository
    {
        private readonly AppDbContext _context;

        public ShopRepository(AppDbContext context, ICurrentTime currentTime) : base(context, currentTime)
        {
            _context = context;
        }

        public async Task AddAsync(ShopData shopData)
        {
            await _context.ShopDatas.AddAsync(shopData);
            await _context.SaveChangesAsync();
        }

        public async Task AddShopAsync(ShopData shopData)
        {
            await _context.ShopDatas.AddAsync(shopData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopData shopData)
        {
            _context.ShopDatas.Update(shopData);
            await _context.SaveChangesAsync();
        }
        public async Task<ShopData> GetShopByIdAsync(Guid userId)
        {
            return await _context.ShopDatas.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<ApplicationUser> GetShopROLEByIdAsync(Guid shopId)
        {
            var shop = await _context.Users
            .Include(u => u.RoleCode)
            .FirstOrDefaultAsync(u => u.Id == shopId && u.RoleCode.RoleName == "Instructor");

            if (shop == null)
            {
                return null;
            }
            else return shop;
        }

        public async Task<List<ApplicationUser>> GetAllShopAsync()
        {
            return await _context.Users
            .Where(u => u.RoleCode.Id == 2)
            .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetPendingShopAsync()
        {
            return await _context.Users
                     .Where(u => u.Status == UserStatus.Pending && u.RoleCodeId == 2)
                     .Include(u => u.ShopData)
                     .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(r => r.RoleCode).FirstOrDefaultAsync(e => e.Id == id && e.RoleCode.Id == 3);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
