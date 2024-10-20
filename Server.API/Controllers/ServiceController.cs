using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Application.Validations.ServiceValidate;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using Server.Contracts.Abstractions.Shared;
using Server.Application.Mappers.ServiceExtensions;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;

namespace Server.API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;

        public ServiceController(IMapper mapper, IServiceService serviceService)
        {
            _mapper = mapper;
            _serviceService = serviceService;
        }

        [HttpPost("CreateService")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> CreateService([FromForm] CreateServiceRequest req)
        {
            var validator = new CreateServiceRequestValidator();
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


            req.Id = Guid.NewGuid();

            var serviceMapper = req.ToCreateServiceDTO();
            var result = await _serviceService.CreateService(serviceMapper);

            return Ok(result);
        }


        [HttpGet("ViewListServicesTitleByUserId")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> ViewListServicesTitleByUserId([FromQuery] Guid userId)
        {
            var result = await _serviceService.ViewListServicesTitleByUserId(userId);
            return Ok(result);
        }


        [HttpPost("UpdateService")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> UpdateService([FromForm] UpdateServiceRequest req)
        {
            var validator = new UpdateServiceRequestValidator();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Error = 1,
                    Message = "Invalid input!",
                    Data = validatorResult.Errors.Select(x => x.ErrorMessage),
                });
            }

            var serviceMapper = req.ToUpdateServiceDTO();

            var result = await _serviceService.UpdateService(serviceMapper);

            return Ok(result);
        }

        [HttpGet("GetListServices")]
        [ProducesResponseType(200, Type = typeof(ViewServiceDTO))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> GetListServices(int pageIndex = 0, int pageSize = 10)
        {
            var pagedCourses = await _serviceService.ViewCourses(pageIndex, pageSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pagedCourses);
        }

        [HttpGet("GetListUserFromService")]
        [ProducesResponseType(200, Type = typeof(ViewServiceDTO))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> GetListUserFromService(Guid courseId, int pageIndex = 0, int pageSize = 10)
        {
            var pagedCourses = await _serviceService.GetListUserFromService(courseId, pageIndex, pageSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pagedCourses);
        }

        [HttpPost("ApproveOrReject")]
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

        [HttpDelete("SoftDeleteService/{id}")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> SoftDeleteService(string id)
        {
            if (!Guid.TryParse(id, out var serviceId))
            {
                return BadRequest(new Result<object>
                {
                    Error = 1,
                    Message = "Invalid service ID format!",
                    Data = null
                });
            }

            var result = await _serviceService.SoftDeleteService(serviceId);

            if (result.Error == 1)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetListservicesByCategory")]
        [ProducesResponseType(200, Type = typeof(ViewServiceDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetListservicesByCategory([FromQuery] Guid? categoryId, [FromQuery] Guid? subCategoryId)
        {
            var courses = await _serviceService.ViewListServicesForCategory(categoryId, subCategoryId);
            return Ok(courses);
        }

        [HttpGet("ViewUserRegistered")]
        [ProducesResponseType(200, Type = typeof(Result<List<Service>>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> ViewUserRegistered(Guid serviceId)
        {
            var result = await _serviceService.ViewUserRegistered(serviceId);
            return Ok(result);
        }
    }
}
