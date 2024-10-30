using AutoMapper;
using Server.Application.Common;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<ViewServiceDTO>> SearchServicePagination(string searchQuery, int pageIndex, int pageSize)
        {

            // Filter and get the total count of matching services
            var totalItemsCount = await _unitOfWork.searchRepository.GetTotalServiceCount(searchQuery);

            // Get paginated and filtered services based on the search query
            var services = await _unitOfWork.searchRepository.GetPagedServices(searchQuery, pageIndex, pageSize);

            // Map the services to the DTO
            var mappedServices = _mapper.Map<List<ViewServiceDTO>>(services);

            // Return the paginated result with the search filter applied
            return new Pagination<ViewServiceDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = mappedServices
            };
        }
    }
}
