﻿using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Repositories
{
    public interface IBookingRepository
    {
        Task<IQueryable<Booking>> GetListBookingByShopId(Guid shopId);
        Task<IQueryable<Booking>> GetListBookingByUserId(Guid userId);
        Task AddBooking(Booking booking);
    }
}
