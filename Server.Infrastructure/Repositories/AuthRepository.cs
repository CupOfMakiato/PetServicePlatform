using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class AuthRepository : GenericRepository<ApplicationUser>, IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext dataContext, ICurrentTime currentTime) : base(dataContext, currentTime)
        {
            _context = dataContext;
        }

        public async Task<bool> DeleteRefreshToken(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.RefreshToken = "";
            return await SaveChange();
        }

        public async Task<ApplicationUser> GetRefreshToken(string refreshToken)
        {
            return await _context.Users.Include(r => r.RoleCode).FirstOrDefaultAsync(r => r.RefreshToken == refreshToken);
        }
        public async Task<bool> UpdateRefreshToken(Guid userId, string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.RefreshToken = refreshToken;
            return await SaveChange();
        }

        public async Task<bool> SaveChange()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 && true;
        }
    }
}
