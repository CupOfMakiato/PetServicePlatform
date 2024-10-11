using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Repositories
{
    public interface IFeedbackRepository :IGenericRepository<Feedback>
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(Guid id);
        Task AddAsync(Feedback entity);
        void Update(Feedback entity);
        void UpdateRange(List<Feedback> entities);
        void SoftRemove(Feedback entity);
        Task<double> AverageRating();
        Task<List<Feedback>> GetAllFeedbacksByUserId(Guid userId);
        Task<List<Feedback>> GetAllFeedbacksByServiceId(Guid serviceId);
        Task<Feedback> GetFeedbacksByUserAndServiceAsync(Guid userId, Guid serviceId);
    }
}
