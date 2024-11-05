using Server.Application.Common;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IServiceService
    {
        Task<Result<object>> ViewServiceById(Guid serviceId);
        Task<Pagination<ViewServiceDTO>> ViewServices(int pageIndex, int pageSize);
        Task<Result<object>> ViewListServicesTitleByUserId(Guid userId);
        Task<Result<object>> CreateService(CreateServiceDTO serviceDto);
        Task<Result<object>> UpdateService(UpdateServiceDTO serviceDto);
        Task<Result<object>> SoftDeleteService(Guid serviceId);
        Task<Result<object>> ApproveOrReject(Guid serviceId, bool isApproved, string? reason);
        Task<Result<object>> ViewUserRegistered(Guid serviceId);
        Task<List<Service>> ViewListServicesForCategory(Guid? categoryId = null, Guid? subCategoryId = null);
        Task<Pagination<ViewUserRegitered>> GetListUserFromService(Guid serviceId, int pageIndex = 0, int pageSize = 10);
        Task<Result<object>> ViewListServicesByUserId(Guid userId);
    }
}
