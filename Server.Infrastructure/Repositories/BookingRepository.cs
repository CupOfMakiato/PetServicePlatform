﻿using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context, ICurrentTime currentTime) : base(context, currentTime)
        {
            _context = context;
        }

        public async Task<IQueryable<Booking>> GetListBookingByShopId(Guid shopId)
        {
            return _context.Booking
                .Include(b => b.User)
                .Where(b => b.User.RoleCodeId == 3)
                .Include(b => b.Service)
                .Where(b => b.UserId == shopId)
                .AsQueryable();
                
        }

        public async Task<IQueryable<Booking>> GetListBookingByUserId(Guid userId)
        {
            return _context.Booking
                .Include(b => b.User)
                .Where(b => b.User.RoleCodeId == 2)
                .Include(b => b.Service)
                .Where(b => b.UserId == userId)
                .AsQueryable();

        }
        public async Task AddBooking(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

    }
}
