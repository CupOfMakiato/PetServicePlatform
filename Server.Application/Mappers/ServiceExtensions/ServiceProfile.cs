using AutoMapper;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using Server.Contracts.DTO.Service;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Mappers.ServiceExtensions
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ViewServiceRequest, ViewServiceDTO>();

            CreateMap<CreateServiceRequest, CreateServiceDTO>();
            CreateMap<UpdateServiceRequest, UpdateServiceDTO>();
        }
    }
}
