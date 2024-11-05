using Server.Application.Common;
using Server.Application.Services;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service> GetServiceById(Guid id);
        Task<Pagination<ViewUserRegitered>> GetListUserByServiceId(Guid serviceId, int pageIndex = 0, int pageSize = 10);
        Task<List<Booking>> GetUserByServiceId(Guid serviceId);
        Task<bool> CheckUserCanRegisterService(Guid userId, Guid serviceId);
        Task<IEnumerable<Service>> SearchServicesAsync(string textSearch);
        Task<List<ServiceIdTitleDTO>> GetListServicesTitleByUserId(Guid userId);
        Task<int> GetTotalServiceCount();
        Task<List<Service>> GetPagedServices(int pageIndex, int pageSize);
        Task<Pagination<ViewSearchServiceUserDTO>> SearchServicePagination(string textSearch, int pageIndex = 0, int pageSize = 5);
        Task<List<Service>> GetListServicesByCategoryId(Guid? categoryId = null, Guid? subCategoryId = null);
        Task<List<ServiceListDTO>> GetListServicesByUserId(Guid userId);
        
    }
}
