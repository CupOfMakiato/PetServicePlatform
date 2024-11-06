using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Application.Interfaces;
using Server.Application.Mappers.ServiceExtensions;
using Server.Application.Repositories;
using Server.Contracts.DTO.Service;
using Server.Contracts.DTO.User;
using Server.Domain.Entities;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ServiceRepository(AppDbContext context, ICurrentTime currentTime, IMapper mapper) : base(context, currentTime)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<Service> GetServiceById(Guid serviceId)
        {
            return await _context.Service.FirstOrDefaultAsync(s => s.Id == serviceId);
        }
        public async Task<List<UserService>> GetUserByServiceId(Guid serviceId)
        {
            return await _context.UserService
                .Include(c => c.Service)
                .Include(u => u.User)
                .Where(c => c.ServiceId == serviceId)
                .ToListAsync();
        }
        public async Task<Pagination<ViewUserRegitered>> GetListUserByServiceId(Guid serviceId, int pageIndex = 0, int pageSize = 10)
        {
            var query = _context.UserService
                              .Where(c => c.ServiceId == serviceId)
                              .Select(c => new ViewUserRegitered
                              {
                                  FullName = c.User.FullName,
                                  Email = c.User.Email,
                              });

            var totalItemsCount = await query.CountAsync();

            var items = await query
                                .Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            var result = new Pagination<ViewUserRegitered>
            {
                TotalItemsCount = totalItemsCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items
            };

            // Calculate the total student count for the course
            var totalStudentCount = await _context.UserService
                                                     .Where(c => c.ServiceId == serviceId)
                                                     .CountAsync();

            return result;
        }
        public async Task<bool> CheckUserCanRegisterService(Guid userId, Guid serviceId)
        {
            bool isServiceRegistered = await _context.UserService.AnyAsync(x => x.ServiceId == serviceId && x.UserId == userId);

            return !isServiceRegistered;
        }
        public async Task<IEnumerable<Service>> SearchServicesAsync(string textSearch)
        {
            return await _context.Service
                .Where(s => s.Title.Contains(textSearch) || s.Description.Contains(textSearch))
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Service>> GetPagedServices(int pageIndex, int pageSize)
        {
            return await _context.Service
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .Include(s => s.UserService) // Include UserService to access users
                                    .ThenInclude(us => us.User) // Then include the User
                                    .ToListAsync();
        }

        public async Task<Pagination<ViewServiceDTO>> SearchServicePagination(string textSearch, int pageIndex = 0, int pageSize = 5)
        {
            var itemCount = await _context.Service.CountAsync(t => t.Title.Contains(textSearch));
            var items = await _context.Service.Where(t => t.Title.Contains(textSearch))
                                    .Skip(pageIndex * pageSize)
                                    .Include(c => c.UserService) // Include UserService to access related users
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var serviceDTO = new List<ViewServiceDTO>();

            foreach (var service in items)
            {
                // Assuming the first user in UserService is the creator
                var createdByUser = service.UserService.FirstOrDefault()?.User; // Get the ApplicationUser
                serviceDTO.Add(service.ToViewServiceDTO(createdByUser));
            }

            var result = new Pagination<ViewServiceDTO>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = serviceDTO,
            };

            return result;
        }

        public async Task<List<Service>> GetListServicesByCategoryId(Guid? categoryId = null, Guid? subCategoryId = null)
        {
            var query = _context.Service.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(s => s.SubCategory.CategoryId == categoryId.Value);
            }

            if (subCategoryId.HasValue)
            {
                query = query.Where(s => s.SubCategoryId == subCategoryId.Value);
            }

            return await query.Include(s => s.SubCategory).ThenInclude(sc => sc.Category).ToListAsync();
        }

        public async Task<int> GetTotalServiceCount()
        {
            return await _context.Service.CountAsync(c => !c.IsDeleted);
        }

        public async Task<List<ServiceIdTitleDTO>> GetListServicesTitleByUserId(Guid userId)
        {
            return await _context.Service.Where(c => c.CreatedBy == userId).ProjectTo<ServiceIdTitleDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task UpdateService(Service service)
        {
            _context.Service.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
