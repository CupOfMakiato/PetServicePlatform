using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Application.Validations.ServiceValidate;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Email;
using Server.Contracts.DTO.Shop;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IServiceService _serviceService;

        public AdminController(IShopService shopService, IServiceService serviceService)
        {
            _shopService = shopService;
            _serviceService = serviceService;
        }

        /// <summary>
        /// Gets all pending shops.
        /// </summary>
        [HttpGet("shops/pending")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetPendingInstructors()
        {
            var pendingShops = await _shopService.GetPendingShopsAsync();
            return Ok(pendingShops);
        }

        /// <summary>
        /// Changes the status of a specific shops.
        /// </summary>
        [HttpPut("shops/{id}/status")]
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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

        [HttpPost("services/approve-or-reject")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> ApproveOrReject([FromForm] CreateApproveOrRejectRequest req)
        {
            var validator = new CreateApproveOrRejectValidator();
            var validatorResult = validator.Validate(req);
            if (validatorResult.IsValid == false)
            {
                return BadRequest(new Result<object>
                {
                    Error = 1,
                    Message = "Missing value!",
                    Data = validatorResult.Errors.Select(x => x.ErrorMessage),
                });
            }
            var result = await _serviceService.ApproveOrReject(req.ServiceId, req.IsApproved, req.Reason);

            if (result.Error == 1)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
