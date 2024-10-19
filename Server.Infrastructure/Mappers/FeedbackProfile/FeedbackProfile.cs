using AutoMapper;
using Server.Contracts.DTO.Feedback;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Mappers.FeedbackProfile
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<CreateFeedbackDTO, Feedback>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UpdateFeedbackDTO, Feedback>();
            CreateMap<Feedback, ViewFeedbackDTO>();
        }
    }
}
