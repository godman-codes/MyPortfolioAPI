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
            CreateMap<WorkExperienceToUpdateDto, WorkExperienceModel>().ReverseMap();
            //CreateMap<DeleteResouceDto, WorkExperienceModel>()
            //    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TechnologyRequestDto, TechnologiesModel>()
                // this allows for the adding of the owner id during mapping
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom((src, _, _, context) => context.Items["OwnerId"]));
            CreateMap<TechnologiesModel, TechnologyResponseDto>();
            CreateMap<TechnologyUpdateDto, TechnologiesModel>().ReverseMap();
            CreateMap<ProjectRequestDto, ProjectsModel>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom((src, _, _, context) => context.Items["OwnerId"]));
            
            CreateMap<ProjectsModel, ProjectResponseDto>().ReverseMap();
            CreateMap<ProjectToUpdateDto, ProjectsModel>();
                //.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
