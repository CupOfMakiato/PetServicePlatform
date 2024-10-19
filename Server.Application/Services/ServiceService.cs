using AutoMapper;
using CloudinaryDotNet;
using Server.Application.Common;
using Server.Application.Interfaces;
using Server.Application.Mappers.ServiceExtensions;
using Server.Contracts.Abstractions.CloudinaryService;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Pagination<ViewServiceDTO>> ViewCourses(int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.serviceRepository.GetTotalServiceCount();
            var courses = await _unitOfWork.serviceRepository.GetPagedServices(pageIndex, pageSize);
            var mappedCourses = _mapper.Map<List<ViewServiceDTO>>(courses);

            return new Pagination<ViewServiceDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = mappedCourses
            };
        }
        public async Task<Result<object>> ViewListServicesTitleByUserId(Guid userId)
        {
            var result = await _unitOfWork.serviceRepository.GetListServicesTitleByUserId(userId);
            return new Result<object>
            {
                Error = result != null ? 0 : 1,
                Message = result != null ? "Get list service successfully" : "Get list service failed",
                Data = result
            };
        }
        public async Task<Result<object>> CreateService(CreateServiceDTO serviceDto)
        {
            var user = await _unitOfWork.userRepository.GetByIdAsync(serviceDto.UserId);
            if (user == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "User does not exist!",
                    Data = null
                };
            }

            string fileExtension = Path.GetExtension(serviceDto.ThumbNail.FileName);
            string newFileName = $"{serviceDto.Title}_{serviceDto.Id}_{fileExtension}";
            CloudinaryResponse cloudinaryResult = await _cloudinaryService.UploadImage(newFileName, serviceDto.ThumbNail);
            if (cloudinaryResult == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "One error detected, please again!",
                    Data = null
                };
            }

            serviceDto.ThumbNailUrl = cloudinaryResult.ImageUrl; ;
            serviceDto.ThumbNailId = cloudinaryResult.PublicImageId;

            var serviceMapper = serviceDto.ToService();

            await _unitOfWork.serviceRepository.AddAsync(serviceMapper);
            var result = await _unitOfWork.SaveChangeAsync();

            return new Result<object>
            {
                Error = result > 0 ? 0 : 1,
                Message = result > 0 ? "Create course successfully" : "Create course fail",
                Data = null
            };
        }
        public async Task<Result<object>> SoftDeleteService(Guid serviceId)
        {
            var existingService = await _unitOfWork.serviceRepository.GetByIdAsync(serviceId);
            if (existingService == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Course not found",
                    Data = null
                };
            }

            _unitOfWork.serviceRepository.SoftRemove(existingService);
            var result = await _unitOfWork.SaveChangeAsync();

            return new Result<object>
            {
                Error = result > 0 ? 0 : 1,
                Message = result > 0 ? "Soft delete service successfully" : "Soft delete service failed",
                Data = existingService
            };
        }

        public async Task<Result<object>> UpdateService(UpdateServiceDTO serviceDto)
        {
            var service = await _unitOfWork.serviceRepository.GetByIdAsync(serviceDto.Id);
            if (service == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Service not found",
                    Data = null
                };
            }

            if (serviceDto.ThumbNail != null)
            {
                string fileExtension = Path.GetExtension(serviceDto.ThumbNail.FileName);
                string newFileName = $"{serviceDto.Title}_{serviceDto.Id}_{fileExtension}";
                CloudinaryResponse cloudinaryResult = await _cloudinaryService.UploadImage(newFileName, serviceDto.ThumbNail);
                if (cloudinaryResult == null)
                {
                    return new Result<object>
                    {
                        Error = 1,
                        Message = "Error Occurred, please try again!",
                        Data = null
                    };
                }
                if (cloudinaryResult != null)
                {
                    await _cloudinaryService.DeleteFileAsync(service.ThumbNailId);

                    service.ThumbNail = cloudinaryResult.ImageUrl;
                    service.ThumbNailId = cloudinaryResult.PublicImageId;
                }
            }

            service.Title = serviceDto.Title;
            service.Description = serviceDto.Description;
            service.Price = serviceDto.Price;
            service.SubCategoryId = serviceDto.SubCategoryId;
            service.isVerified = false;
            service.Reason = "new update";

            _unitOfWork.serviceRepository.Update(service);
            var result = await _unitOfWork.SaveChangeAsync();

            return new Result<object>
            {
                Error = result > 0 ? 0 : 1,
                Message = result > 0 ? "Update service successfully" : "Update service failed",
                Data = null
            };
        }
        public async Task<Result<object>> ApproveOrReject(Guid serviceId, bool isApproved, string? reason)
        {
            var existingService = await _unitOfWork.serviceRepository.GetByIdAsync(serviceId);
            if (existingService == null)
            {
                return new Result<object>
                {
                    Error = 1,
                    Message = "Service not found",
                    Data = null
                };
            }

            existingService.isVerified = isApproved;
            existingService.Reason = reason;

            _unitOfWork.serviceRepository.Update(existingService);
            var result = await _unitOfWork.SaveChangeAsync();

            return new Result<object>
            {
                Error = result > 0 ? 0 : 1,
                Message = result > 0 ? "Service verified successfully" : "Service verification failed",
                Data = null
            };
        }
        public async Task<Result<object>> ViewUserRegistered(Guid serviceId)
        {
            var user = await _unitOfWork.serviceRepository.GetUserByServiceId(serviceId);
            var listUser = _mapper.Map<List<ViewUserRegitered>>(user);
            return new Result<object>
            {
                Error = 0,
                Message = "View user registered",
                Data = listUser
            };
        }
        public async Task<List<Service>> ViewListServicesForCategory(Guid? categoryId = null, Guid? subCategoryId = null)
        {
            return await _unitOfWork.serviceRepository.GetListServicesByCategoryId(categoryId, subCategoryId);
        }

        public async Task<Pagination<ViewUserRegitered>> GetListUserFromService(Guid serviceId, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _unitOfWork.serviceRepository.GetListUserByServiceId(serviceId, pageIndex, pageSize);

            return result;
        }

    }
}
