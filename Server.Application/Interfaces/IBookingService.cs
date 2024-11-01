using Server.Application.Common;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByShopId(int PAGE_SIZE = 10, int page = 1);
        Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByUserId(int PAGE_SIZE = 10, int page = 1);
    }
}
