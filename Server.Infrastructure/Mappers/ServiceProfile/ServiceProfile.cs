using AutoMapper;
using CloudinaryDotNet;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Mappers.ServiceProfile
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceDTO, Service>()
            .ForMember(dest => dest.ThumbNail, opt => opt.MapFrom(src => src.ThumbNailUrl))
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Service, ServiceIdTitleDTO>();
            CreateMap<Service, ServiceListDTO>();

            CreateMap<Service, ServiceDTO>();

            CreateMap<Service, UserServiceDTO>();
            CreateMap<Service, ViewServiceDTO>();
            //.ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.UserService));
            CreateMap<UserService, ViewUserRegitered>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
