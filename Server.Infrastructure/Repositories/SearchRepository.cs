using AutoMapper;
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
    public class SearchRepository : GenericRepository<Service>, ISearchRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SearchRepository(AppDbContext context, ICurrentTime currentTime, IMapper mapper) : base(context, currentTime)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<int> GetTotalServiceCount(string searchQuery)
        {
            return await _context.Service
                .Where(s => s.Title.Contains(searchQuery) || s.Description.Contains(searchQuery))
                .CountAsync();
        }

        // Fetch paged services that match the search query
        public async Task<List<Service>> GetPagedServices(string searchQuery, int pageIndex, int pageSize)
        {
            return await _context.Service
                .Where(s => s.Title.Contains(searchQuery) || s.Description.Contains(searchQuery))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Include(s => s.CreatedByUser)
                .ToListAsync();
        }
    }
}
