using Server.Application.Common;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Booking;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Result<Booking>> GetBookingById(Guid id);
        Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByShopId(int PAGE_SIZE = 10, int page = 1);
        Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByUserId(int PAGE_SIZE = 10, int page = 1);
        Task<Result<object>> AddBooking(AddBookingDto addBookingDto);
        Task<Result<object>> CheckInBooking(Guid bookingId);
    }
}
