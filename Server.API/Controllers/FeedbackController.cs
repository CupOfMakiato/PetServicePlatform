using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Validations.Feedback;
using Server.Contracts.Abstractions.RequestAndResponse.Feedback;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Feedback;
using Server.Infrastructure;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(AppDbContext context, IFeedbackRepository feedbackRepository, IFeedbackService feedbackService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _feedbackService = feedbackService;
        }

        [HttpGet("GetAllFeedbacks")]
        [ProducesResponseType(200, Type = typeof(ViewFeedbackDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = _mapper.Map<List<ViewFeedbackDTO>>(await _feedbackRepository.GetAllAsync());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPost("SendFeedback")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromForm] CreateFeedbackRequest req)
        {
            var validator = new CreateFeedbackRequestValidator();
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

            var feedbackMapper = _mapper.Map<CreateFeedbackDTO>(req);
            var result = await _feedbackService.SendFeedback(feedbackMapper);

            return Ok(result);
        }

        [HttpPost("EditFeedback")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateReview(string id, [FromForm] UpdateFeedbackRequest req)
        {
            if (!Guid.TryParse(id, out var feedbackId))
            {
                return BadRequest(new Result<object>
                {
                    Error = 1,
                    Message = "Invalid service ID format!",
                    Data = null
                });
            }

            var validator = new UpdateFeedbackRequestValidator();
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

            var feedbackMapper = _mapper.Map<UpdateFeedbackDTO>(req);
            var result = await _feedbackService.UpdateFeedback(feedbackId, feedbackMapper);

            if (result.Error == 1)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("DeleteFeedback/{id}")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteReview(string id)
        {
            if (!Guid.TryParse(id, out var feedbackId))
            {
                return BadRequest(new Result<object>
                {
                    Error = 1,
                    Message = "Invalid feedback ID format!",
                    Data = null
                });
            }

            var result = await _feedbackService.RemoveFeedback(feedbackId);

            if (result.Error == 1)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // Get Reviews by courseId
        [HttpGet("GetFeedbacksByServiceId")]
        [ProducesResponseType(200, Type = typeof(ViewFeedbackDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsForCourse(Guid serviceId)
        {
            var feedbacks = _mapper.Map<List<ViewFeedbackDTO>>(await _feedbackService.GetFeedbacksForService(serviceId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(feedbacks);
        }
        // Get Reviews by userId
        [HttpGet("GetFeedbacksByUserId")]
        [ProducesResponseType(200, Type = typeof(ViewFeedbackDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsForUser(Guid userId)
        {
            var feedbacks = _mapper.Map<List<ViewFeedbackDTO>>(await _feedbackService.GetFeedbacksForUser(userId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(feedbacks);
        }
    }
}
