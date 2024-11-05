using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Contracts.DTO.Booking;
using System.Drawing.Printing;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            try
            {
                var result = await _bookingService.GetBookingById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("shop/listbookings")]
        [Authorize(Policy = "ShopPolicy")]
        public async Task<IActionResult> GetListBookingByShopId(int PAGE_SIZE = 10, int page = 1)
        {
            try
            {
                var result = await _bookingService.GetListBookingByShopId(PAGE_SIZE, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("user/listbookings")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetListBookingByUserId(int PAGE_SIZE = 10, int page = 1)
        {
            try
            {
                var result = await _bookingService.GetListBookingByUserId(PAGE_SIZE, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("user/booking")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> AddBooking([FromForm]AddBookingDto addBookingDto)
        {
            try
            {
                var result = await _bookingService.AddBooking(addBookingDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("shop/checkin-booking/{id}")]
        [Authorize(Policy = "ShopPolicy")]
        public async Task<IActionResult> CheckInBooking(Guid id)
        {
            try
            {
                var result = await _bookingService.CheckInBooking(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
