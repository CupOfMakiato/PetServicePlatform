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

        public async Task<Result<object>> SearchServicePagin(int pageIndex, int pageSize, string name)
        {
            var resultData = await _unitOfWork.serviceRepository.SearchServicePagination(name, pageIndex, pageSize);
            return new Result<object>
            {
                Error = 0,
                Message = "Successfully",
                Data = resultData
            };
        }
    }
}
