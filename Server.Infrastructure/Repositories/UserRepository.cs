using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext dataContext, ICurrentTime currentTime) : base(dataContext, currentTime)
        {
            _context = dataContext;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            return await _context.Users
           .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u => u.RoleCode).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }

        public async Task<ApplicationUser> GetUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }

        public async Task<ApplicationUser> GetAllUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<ApplicationUser> GetUserById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<ApplicationUser>> GetUsersByRole(int role)
        {
            return await _context.Users.Where(u => u.RoleCodeId == role).ToListAsync();
        }

        public Task<ApplicationUser> GetUserByIdWithServiceUsed(Guid userId)
        {
            return _context.Users.Include(u => u.Booking).FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<ApplicationUser> GetUserByVerificationToken(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        }
        public async Task<ApplicationUser> GetUserByResetToken(string resetToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == resetToken);
        }
    }
}
