using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using Server.Application.Mappers.UserExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Contracts.DTO.Service;
using CloudinaryDotNet;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using Server.Contracts.Enum;

namespace Server.Application.Mappers.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static ViewServiceDTO ToViewServiceDTO(this Service service)
        {
            return new ViewServiceDTO
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                ThumbNail = service.ThumbNail,
                Price = service.Price,
                SubCategoryId = service.SubCategoryId,
                Type = (Contracts.Enum.ServiceType)service.Type,
                CreatedByUser = service.CreatedByUser.ToUserDTO()
            };
        }
        public static ViewSearchServiceUserDTO ToViewSearchServiceDTO(this Service service)
        {
            return new ViewSearchServiceUserDTO
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                ThumbNail = service.ThumbNail,
                Price = service.Price,
                SubCategoryId = service.SubCategoryId,
                Type = (Contracts.Enum.ServiceType)service.Type,
                CreatedByUser = service.CreatedByUser.ToSearchUserDTO()
            };
        }
        public static Service ToService(this CreateServiceDTO createServiceDTO)
        {

            return new Service
            {
                Title = createServiceDTO.Title,
                Description = createServiceDTO.Description,
                Price = createServiceDTO.Price,
                isVerified = createServiceDTO.isVerified,
                ThumbNail = createServiceDTO.ThumbNailUrl,
                ThumbNailId = createServiceDTO.ThumbNailId,
                SubCategoryId = createServiceDTO.SubCategoryId,
                Type = createServiceDTO.Type,
                //Type = serviceType, // Assign the parsed enum
                CreatedByUserId = createServiceDTO.UserId,
            };
        }
        public static CreateServiceDTO ToCreateServiceDTO(this CreateServiceRequest createServiceRequest)
        {
            return new CreateServiceDTO
            {
                Id = (Guid)createServiceRequest.Id,
                UserId = createServiceRequest.UserId,
                Title = createServiceRequest.Title,
                Description = createServiceRequest.Description,
                ThumbNail = createServiceRequest.ThumbNail,
                Price = createServiceRequest.Price,
                SubCategoryId= createServiceRequest.SubCategoryId,
                Type= createServiceRequest.Type,
                isVerified = false,
            };
        }
        public static UpdateServiceDTO ToUpdateServiceDTO(this UpdateServiceRequest updateServiceRequest)
        {
            return new UpdateServiceDTO
            {
                Id = (Guid)updateServiceRequest.ServiceId,
                Title = updateServiceRequest.Title,
                Description = updateServiceRequest.Description,
                ThumbNail = updateServiceRequest.ThumbNail,
                Price = updateServiceRequest.Price,
                Type = updateServiceRequest.Type,
                SubCategoryId = updateServiceRequest.SubCategoryId,
            };
        }

    }
}
