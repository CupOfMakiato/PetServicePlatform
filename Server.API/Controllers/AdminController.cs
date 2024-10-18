using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Contracts.DTO.Email;
using Server.Contracts.DTO.Shop;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IShopService _shopService;

        public AdminController(IShopService shopService)
        {
            _shopService = shopService;
        }

        /// <summary>
        /// Gets all pending shops.
        /// </summary>
        [HttpGet("shops/pending")]
        public async Task<IActionResult> GetPendingInstructors()
        {
            var pendingShops = await _shopService.GetPendingShopsAsync();
            return Ok(pendingShops);
        }

        /// <summary>
        /// Changes the status of a specific shops.
        /// </summary>
        [HttpPut("shops/{id}/status")]
        public async Task<IActionResult> ChangeStatusInstructor([FromForm] ContentEmailDTO contentEmailDto, Guid id)
        {
            try
            {
                var message = await _shopService.ChangeStatusShop(contentEmailDto, id);
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { message = "An error occurred while updating the status", details = ex.Message });
            }
        }

        /// <summary>
        /// Approves a specific shop.
        /// </summary>
        [HttpPost("shops/{id}/approve")]
        public async Task<IActionResult> ApproveInstructor(Guid id)
        {
            try
            {
                await _shopService.ApproveShopAsync(id);
                return Ok(new { Message = "Shop approved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Rejects a specific shop.
        /// </summary>
        [HttpPost("shops/reject")]
        public async Task<IActionResult> RejectInstructor([FromBody] ApproveRejectShopDTO rejectDto)
        {
            try
            {
                await _shopService.RejectShopAsync(rejectDto);
                return Ok(new { Message = "Shop rejected successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
