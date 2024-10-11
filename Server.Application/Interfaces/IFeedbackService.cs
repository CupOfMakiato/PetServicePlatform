using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<ViewFeedbackDTO>> GetFeedbacksForService(Guid id);
        public Task<List<ViewFeedbackDTO>> GetFeedback();
        Task<Result<object>> RemoveFeedback(Guid id);
        Task<Result<object>> UpdateFeedback(Guid id, UpdateFeedbackDTO feedbackDto);
        Task<Result<object>> SendFeedback(CreateFeedbackDTO feedbackDto);
        Task<List<CreateFeedbackDTO>> GetFeedbacksForUser(Guid userId);
        Task<Result<object>> GetAverageRating();
    }
}
