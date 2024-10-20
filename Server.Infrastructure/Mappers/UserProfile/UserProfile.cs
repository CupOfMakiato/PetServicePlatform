using AutoMapper;
using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Mappers.UserProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => 0));
        }
    }
}
