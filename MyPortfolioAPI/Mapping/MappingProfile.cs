using AutoMapper;
using Entities.Models;
using Shared.DTOs.Request;
using Shared.DTOs.Response;

namespace MyPortfolioAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, UserModel>();
            CreateMap<WorkExperienceRequestDto, WorkExperienceModel>()
                // this allows for the adding of the owner id during mapping
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom((src, _, _, context) => context.Items["OwnerId"]));
            CreateMap<WorkExperienceModel, WorkExperienceResponseDto>();
            CreateMap<WorkExperienceToUpdateDto, WorkExperienceModel>();
        }

    }
}
