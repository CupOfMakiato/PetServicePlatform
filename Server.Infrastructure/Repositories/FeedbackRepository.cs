using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly AppDbContext _context;
        public FeedbackRepository(AppDbContext context, ICurrentTime currentTime) : base(context, currentTime)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllFeedbacksByServiceId(Guid serviceId)
        {
            return await _context.Feedback
                .Where(f => f.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<double> AverageRating()
        {
            return await _context.Feedback.AverageAsync(x => x.Rating);
        }

        public async Task<Feedback> GetFeedbacksByUserAndServiceAsync(Guid userId, Guid serviceId)
        {
            return await _context.Feedback
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ServiceId == serviceId);
        }

        public async Task<List<Feedback>> GetAllFeedbacksByUserId(Guid userId)
        {
            return await _context.Feedback
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}
