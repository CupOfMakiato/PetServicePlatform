using AutoMapper;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Feedback;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<object>> GetAverageRating()
        {
            var review = await _unitOfWork.feedbackRepository.AverageRating();
            if (review == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Feedback not found",
                    Data = null
                };
            };
            return new Result<object>
            {
                Error = 0,
                Message = "Get average rating successfully",
                Data = new { AverageRating = review }
            };
        }

        public async Task<List<ViewFeedbackDTO>> GetFeedback()
        {
            return _mapper.Map<List<ViewFeedbackDTO>>(await _unitOfWork.feedbackRepository.GetAllAsync());
        }

        public async Task<List<ViewFeedbackDTO>> GetFeedbacksForService(Guid id)
        {
            // Fetch reviews for the specified course
            var feedbacks = await _unitOfWork.feedbackRepository.GetAllFeedbacksByServiceId(id);

            // Fetch all users to map their full names
            var users = await _unitOfWork.userRepository.GetAllAsync();

            // Map reviews to ViewReviewDTO and populate FullName from associated User
            var feedbackDTO = feedbacks.Select(feedback =>
            {
                var dto = _mapper.Map<ViewFeedbackDTO>(feedback);
                dto.FullName = users.FirstOrDefault(u => u.Id == feedback.UserId)?.FullName; // Assuming UserId links to User's Id
                return dto;
            }).ToList();

            return feedbackDTO;
        }

        public async Task<List<ViewFeedbackDTO>> GetFeedbacksForUser(Guid userId)
        {
            var feedbacks = await _unitOfWork.feedbackRepository.GetAllFeedbacksByUserId(userId);

            var users = await _unitOfWork.userRepository.GetAllAsync();

            var feedbackDTO = feedbacks.Select(feedback =>
            {
                var dto = _mapper.Map<ViewFeedbackDTO>(feedback);
                dto.FullName = users.FirstOrDefault(u => u.Id == feedback.UserId)?.FullName;
                return dto;
            }).ToList();

            return feedbackDTO;
        }

        public async Task<Result<object>> RemoveFeedback(Guid id)
        {
            var existingFeedback = await _unitOfWork.feedbackRepository.GetByIdAsync(id);
            if (existingFeedback == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Feedback not found",
                    Data = null
                };
            }

            _unitOfWork.feedbackRepository.SoftRemove(existingFeedback);
            var result = await _unitOfWork.SaveChangeAsync();

            return new Result<object>
            {
                Error = result > 0 ? 0 : 1,
                Message = result > 0 ? "Delete feedback successfully" : "Delete feedback failed",
                Data = existingFeedback
            };
        }

        public async Task<Result<object>> SendFeedback(CreateFeedbackDTO feedbackDto)
        {
            // Validate the rating to ensure it is between 1 and 5.
            if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
            {
                return new Result<object>
                {
                    Error = 5,
                    Message = "Rating must be from 1 to 5 stars!",
                    Data = null
                };
            }

            // Retrieve the user along with their enrolled courses based on the userId provided in reviewDto.
            var user = await _unitOfWork.userRepository.GetUserByIdWithServiceUsed(feedbackDto.UserId);

            // Check if the user exists.
            if (user == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "User does not exist!",
                    Data = null
                };
            }

            // Check if the user has used in the service they are trying to review.
            var isUsed = user.Booking.Any(uec => uec.ServiceId == feedbackDto.ServiceId);
            if (!isUsed)
            {
                return new Result<object>
                {
                    Error = 3,
                    Message = "User is not enrolled in the course!",
                    Data = null
                };
            }

            // Check if the user has already submitted a review for the service.
            var existingFeedback = await _unitOfWork.feedbackRepository.GetFeedbacksByUserAndServiceAsync(feedbackDto.UserId, feedbackDto.ServiceId);
            if (existingFeedback != null)
            {
                return new Result<object>
                {
                    Error = 4,
                    Message = "You have already submitted a review, please edit instead!",
                    Data = null
                };
            }

            // Map the CreateReviewDTO to the Review entity.
            var feedbackMapper = _mapper.Map<Feedback>(feedbackDto);

            // Add the new review to the repository.
            await _unitOfWork.feedbackRepository.AddAsync(feedbackMapper);

            // Save changes to the database and get the result.
            var result = await _unitOfWork.SaveChangeAsync();

            // Check if the save was successful and return the appropriate result.
            if (result > 0)
            {
                return new Result<object>
                {
                    Error = 0,
                    Message = "feedback sent successfully!",
                    Data = feedbackMapper
                };
            }
            else
            {
                return new Result<object>
                {
                    Error = 2,
                    Message = "Failed to send feedback!",
                    Data = null
                };
            }
        }

        public async Task<Result<object>> UpdateFeedback(Guid id, UpdateFeedbackDTO feedbackDto)
        {
            // Validate the rating to ensure it is between 1 and 5.
            if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
            {
                return new Result<object>
                {
                    Error = 5,
                    Message = "Rating must be between 1 and 5!",
                    Data = null
                };
            }

            // Retrieve the existing review based on the review ID.
            var existingFeedback = await _unitOfWork.feedbackRepository.GetByIdAsync(id);
            if (existingFeedback == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Feedback not found",
                    Data = null
                };
            }

            // Map the updated review DTO to the existing review entity.
            var feedbackMapper = _mapper.Map(feedbackDto, existingFeedback);

            // Update the review in the repository.
            _unitOfWork.feedbackRepository.Update(feedbackMapper);

            // Save changes to the database and get the result.
            var result = await _unitOfWork.SaveChangeAsync();

            // Check if the save was successful and return the appropriate result.
            return new Result<object>
            {
                Error = result > 0 ? 0 : 2,
                Message = result > 0 ? "Update feedback successfully" : "Update feedback failed",
                Data = result > 0 ? (object)feedbackMapper : null
            };
        }
    }
}
